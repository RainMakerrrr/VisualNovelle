using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace MatchGame
{
    public class MatchCard : MonoBehaviour
    {
        public event Action<MatchCard> CardClicked;

        [SerializeField] private Image _image;
        [SerializeField] private Button _button;
        public string Id { get; private set; }

        private Sprite _icon;
        private Sprite _coverSprite;

        private Sequence _sequence;

        public void Init(Sprite sprite, string id)
        {
            _icon = sprite;
            Id = id;
            _coverSprite = _image.sprite;

            _button.onClick.AddListener(OnButtonClicked);
        }

        private void OnDestroy()
        {
            _sequence?.Kill();
            _button.onClick.RemoveListener(OnButtonClicked);
        }

        private void OnButtonClicked()
        {
            CardClicked?.Invoke(this);
        }

        public void Show()
        {
            _button.interactable = false;

            _sequence?.Kill();
            _sequence = DOTween.Sequence();
            _sequence.SetTarget(this);

            _sequence.Join(transform.DORotate(new Vector3(0f, 180f, 0f), 0.5f).SetEase(Ease.Linear));
            _sequence.InsertCallback(0.25f, () => _image.sprite = _icon);
        }

        public void Hide()
        {
            _button.interactable = true;

            _sequence?.Kill();
            _sequence = DOTween.Sequence();
            _sequence.SetTarget(this);

            _sequence.Join(transform.DORotate(Vector3.zero, 0.5f).SetEase(Ease.Linear));
            _sequence.InsertCallback(0.25f, () => _image.sprite = _coverSprite);
        }
    }
}