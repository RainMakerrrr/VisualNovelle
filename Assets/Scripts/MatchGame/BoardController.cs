using System;
using System.Collections.Generic;
using Naninovel;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MatchGame
{
    public class BoardController : MonoBehaviour
    {
        private const string BubblesSound = "Bubbles";
        public event Action AllCardsMatched;

        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _delayForShow = 0.75f;

        private List<MatchCard> _cards = new List<MatchCard>();

        private MatchCard _selected;

        private int _counter;
        private int _maxCount;


        public void Init(List<MatchCard> cards)
        {
            _cards = cards;
            _maxCount = _cards.Count / 2;

            foreach (MatchCard matchCard in _cards)
            {
                matchCard.CardClicked += OnCardClicked;
            }
        }

        private void OnDestroy()
        {
            foreach (MatchCard matchCard in _cards)
            {
                matchCard.CardClicked -= OnCardClicked;
            }
        }

        private void OnCardClicked(MatchCard matchCard)
        {
            if (_selected == null)
            {
                _selected = matchCard;
                _selected.Show();
                return;
            }

            matchCard.Show();

            if (_selected.Id == matchCard.Id)
            {
                Engine.GetService<IAudioManager>().PlaySfxAsync(BubblesSound).Forget();

                _counter++;
                if (_counter == _maxCount)
                {
                    AllCardsMatched?.Invoke();
                }
            }
            else
            {
                HideCards(_selected, matchCard);
            }

            _selected = null;
        }

        private async void HideCards(params MatchCard[] cards)
        {
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;

            await UniTask.Delay(TimeSpan.FromSeconds(_delayForShow));

            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;

            foreach (MatchCard matchCard in cards)
            {
                matchCard.Hide();
            }
        }
    }
}