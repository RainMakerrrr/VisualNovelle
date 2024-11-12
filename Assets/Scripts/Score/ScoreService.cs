using System;
using Naninovel;

namespace Score
{
    [InitializeAtRuntime]
    public class ScoreService : IEngineService
    {
        private const string ScoreVariableKey = "playerScore";

        public event Action<int> ScoreUpdated;

        private int _playerScore;
        private ICustomVariableManager _variableManager;

        public int Score => _playerScore;

        public void AddScore(int score)
        {
            _playerScore += score;
            _variableManager.SetVariableValue(ScoreVariableKey, _playerScore.ToString());
            ScoreUpdated?.Invoke(_playerScore);
        }

        public bool IsScoreGreaterThan(int score) => _playerScore >= score;

        public UniTask InitializeServiceAsync()
        {
            _variableManager = Engine.GetService<ICustomVariableManager>();
            if (_variableManager.VariableExists(ScoreVariableKey))
            {
                int.TryParse(_variableManager.GetVariableValue(ScoreVariableKey), out _playerScore);
            }
            else
            {
                _playerScore = 0;
                _variableManager.SetVariableValue(ScoreVariableKey, _playerScore.ToString());
            }

            return UniTask.CompletedTask;
        }

        public void ResetService()
        {
            _playerScore = 0;
            _variableManager.SetVariableValue(ScoreVariableKey, _playerScore.ToString());
        }

        public void DestroyService()
        {
        }
    }
}