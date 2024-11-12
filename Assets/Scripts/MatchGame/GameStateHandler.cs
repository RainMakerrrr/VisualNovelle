using Naninovel;
using Score;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MatchGame
{
    public class GameStateHandler : MonoBehaviour
    {
        private const string FinishScriptName = "Finish";
        
        [SerializeField] private float _minTime;
        [SerializeField] private float _normalTime;
        [SerializeField] private float _maxTime;

        private BoardController _boardController;
        private Timer _timer;

        public void Init(BoardController boardController, Timer timer)
        {
            _boardController = boardController;
            _timer = timer;
            _boardController.AllCardsMatched += OnAllCardsMatched;
        }

        private void OnDestroy()
        {
            _boardController.AllCardsMatched += OnAllCardsMatched;
        }

        private async void OnAllCardsMatched()
        {
            AddScore();
        }

        private void AddScore()
        {
            if (_timer.Value < _minTime)
            {
                Debug.Log($"Add 20");
                Engine.GetService<ScoreService>().AddScore(20);
            }
            else if (_timer.Value > _minTime && _timer.Value < _normalTime)
            {
                Debug.Log($"Add 10");
                Engine.GetService<ScoreService>().AddScore(10);
            }
            else if (_timer.Value > _normalTime && _timer.Value < _maxTime)
            {
                Debug.Log($"Add 5");
                Engine.GetService<ScoreService>().AddScore(5);
            }
            else
            {
                Debug.Log($"Add 1");
                Engine.GetService<ScoreService>().AddScore(1);
            }
        }
    }
}