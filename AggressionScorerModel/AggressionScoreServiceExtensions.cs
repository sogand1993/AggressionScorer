using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.ML;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AggressionScorerModel
{
    //we create this extentionmethod to use this in our API layer and when we need this we can call this but we have to introduce
    //this to our startup setting
    public static class AggressionScoreServiceExtensions
    {
        private static readonly string _modelFile =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Model", "AggressionScoreModel.zip");
        public static void AddAggressionScorePredictionEnginPool(this IServiceCollection services)
        {
            services.AddPredictionEnginePool<ModelInput, ModelOutput>()
                .FromFile(filePath: _modelFile, watchForChanges: true);
        }
    }
}
