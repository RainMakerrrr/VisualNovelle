using System;
using Naninovel;
using Score;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MatchGame
{
    public class GameStateHandler : MonoBehaviour
    {
        private const string FinishScriptName = "Finish";
        private const string MainScene = "Main";

        [SerializeField] private float _minTime;
        [SerializeField] private float _normalTime;
        [SerializeField] private float _maxTime;
        [SerializeField] private float _delayBeforeEnd;

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

            await UniTask.Delay(TimeSpan.FromSeconds(_delayBeforeEnd));
            
            await Engine.GetService<IScriptPlayer>().PreloadAndPlayAsync(FinishScriptName);
            
            SceneManager.LoadScene(MainScene);
        }

        private void AddScore()
        {
            if (_timer.Value < _minTime)
            {
                Engine.GetService<ScoreService>().AddScore(20);
            }
            else if (_timer.Value > _minTime && _timer.Value < _normalTime)
            {
                Engine.GetService<ScoreService>().AddScore(10);
            }
            else if (_timer.Value > _normalTime && _timer.Value < _maxTime)
            {
                Engine.GetService<ScoreService>().AddScore(5);
            }
            else
            {
                Engine.GetService<ScoreService>().AddScore(1);
            }
        }
    }
}