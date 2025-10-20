using System;
using System.IO;
using AggressionScorerModel;
using Microsoft.ML;

namespace AggressionScorer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Aggression scorer model builder started!");
            var mlContext = new MLContext(0);
            //Load data
            // store the data in RAM and then users comments converted to floats so that they are understandable to the machine.
            var inputDataPreparer = mlContext.Transforms.Text.FeaturizeText("Features","Comment").AppendCacheCheckpoint(mlContext);
            //Choosing a scenario
            var trainer = mlContext.BinaryClassification.Trainers.LbfgsLogisticRegression();
            var trainingPipeline = inputDataPreparer.Append(trainer);
            //Load data 

            var createInputFile = @"Data\preparedInput.tsv";
            DataPreparer.CreatePreparedDataFile(createInputFile, true);

            IDataView traninDataView = mlContext.Data.LoadFromTextFile<ModelInput>(
            path: createInputFile,
            hasHeader: true,
            separatorChar: '\t',
            allowQuoting: true
                );


            //Fit the model
            ITransformer model = trainingPipeline.Fit(traninDataView);

            //Test the model


            //Save the model
            if (!Directory.Exists("Model"))
            {
                Directory.CreateDirectory("Model");
            }
            var modelFile = @"Model\\AggressionScoreModel.zip";
            mlContext.Model.Save(model, traninDataView.Schema, modelFile);

            Console.WriteLine("Done !");
        }
    }
}
