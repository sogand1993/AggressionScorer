
### 🧠 Aggression Detection System using ML.NET + ASP.NET Core Web API

This project is a complete machine learning solution to detect aggression in user comments using ML.NET and expose the prediction logic through a Web API built with ASP.NET Core.

---

## 📁 Project Structure

```
AggressionScorer.sln
│
├── AggressionAPI/                 --> ASP.NET Core Web API Layer
│   ├── Controllers/
│   │   └── AggressionScoreController.cs     --> Receives user input and returns aggression prediction
│   ├── aggressionscorer.html               --> HTML Form (for testing API via Submit button)
│   ├── appsettings.json
│   ├── Program.cs
│   └── Startup.cs                          --> Configures services and CORS policy
│
├── AggressionScorer/              --> ML Model Training Project
│   └── Program.cs                 --> Builds and saves ML model as .zip using LbfgsLogisticRegression
│
└── AggressionScorerModel/         --> Model Consumer Layer
    ├── Model/
    │   ├── AggressionScoreModel.zip        --> Trained model saved after pipeline training
    │   ├── ModelInput.cs                   --> Input schema (string Comment)
    │   └── ModelOutput.cs                  --> Output schema (Score + Prediction)
    ├── AggressionScore.cs                  --> Uses PredictionEnginePool to predict from input
    └── AggressionScoreServiceExtensions.cs --> Registers PredictionEnginePool in services
```

---

## 🚀 How it works

### 1. **Model Training**

* In `AggressionScorer/Program.cs`, we:

  * Load a dataset (`preparedInput.tsv`) containing labeled user comments (0 = non-aggressive, 1 = aggressive).
  * Use `FeaturizeText` to transform the comment text.
  * Train a `LbfgsLogisticRegression` binary classifier.
  * Save the trained model to `AggressionScoreModel.zip`.

### 2. **Model Integration**

* In `AggressionScorerModel/`:

  * Define `ModelInput` and `ModelOutput` classes.
  * Use `PredictionEnginePool<TSrc, TDst>` to serve the model efficiently in-memory.
  * Register this prediction pool using `AggressionScoreServiceExtensions`.

### 3. **Web API Communication**

* In `AggressionAPI/`:

  * The controller `AggressionScoreController.cs` exposes an endpoint (e.g. `/predict`) that receives a comment from the frontend (HTML form).
  * It injects `AggressionScore` via Dependency Injection.
  * Calls `Predict()` method and returns whether the comment is aggressive.

### 4. **Frontend**

* A simple HTML form (`aggressionscorer.html`) sends a comment to the backend via a Submit button.
* CORS is enabled in `Startup.cs` to allow frontend requests from any origin.

---

## 🔧 Technologies Used

* **.NET Core 5.0**
* **ML.NET (Microsoft.ML, Microsoft.Extensions.ML)**
* **ASP.NET Core Web API**
* **PredictionEnginePool**
* **HTML form for UI**
* **CORS configuration**


