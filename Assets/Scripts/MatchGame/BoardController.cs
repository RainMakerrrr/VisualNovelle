using System;
using System.Collections.Generic;
using Naninovel;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MatchGame
{
    public class BoardController : MonoBehaviour
    {
        public event Action AllCardsMatched;
        
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
                Engine.GetService<IAudioManager>().PlaySfxAsync("Bubbles").Forget();
                
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
            await UniTask.Delay(TimeSpan.FromSeconds(0.75f));
            
            foreach (MatchCard matchCard in cards)
            {
                matchCard.Hide();
            }
        }
    }
}