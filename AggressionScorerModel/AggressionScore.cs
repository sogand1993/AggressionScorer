using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.ML;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AggressionScorerModel
{
    public class AggressionScore
    {
        private readonly PredictionEnginePool<ModelInput, ModelOutput> _predictionEnginePool;

        public AggressionScore(PredictionEnginePool<ModelInput, ModelOutput> predictionEnginePool)
        {
            _predictionEnginePool = predictionEnginePool;
        }

        public ModelOutput Predict(string input) =>
        _predictionEnginePool.Predict(new ModelInput()
        {
            Comment = input
        });
    }
}
