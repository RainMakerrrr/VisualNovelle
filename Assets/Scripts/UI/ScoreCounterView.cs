using DG.Tweening;
using Naninovel;
using Naninovel.UI;
using Score;
using TMPro;
using UnityEngine;

namespace UI
{
    public class ScoreCounterView : CustomUI
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private float _scoreAnimationDuration;

        private Tween _tween;
        private ScoreService ScoreService => Engine.GetService<ScoreService>();

        protected override void OnEnable()
        {
            base.OnEnable();

            _text.text = ScoreService.Score.ToString();

            ScoreService.ScoreUpdated += OnScoreUpdated;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            _tween?.Kill();
            ScoreService.ScoreUpdated -= OnScoreUpdated;
        }

        private void OnScoreUpdated(int score)
        {
            _tween?.Kill();
            _tween = _text.DOCounter(int.Parse(_text.text), score, _scoreAnimationDuration).SetEase(Ease.Linear);
        }
    }
}