using System.Collections.Generic;
using MatchGame.Configs;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace MatchGame
{
    public class GameBoard : MonoBehaviour
    {
        [SerializeField] private GridLayoutGroup _gridLayout;
        [SerializeField] private MatchCard _cardPrefab;
        [SerializeField] private MatchGameSettings _matchGameSettings;

        private List<MatchCard> _cards = new List<MatchCard>();

        public List<MatchCard> Cards => _cards;
        
        public void Init()
        {
            for (int i = 0; i < _matchGameSettings.BoardSize; i++)
            {
                MatchCard matchCard = Instantiate(_cardPrefab, _gridLayout.transform);
                _cards.Add(matchCard);
            }

            int dataIndex = 0;

            for (int i = 0; i < _cards.Count; i++)
            {
                if (dataIndex >= _matchGameSettings.Data.Length)
                    break;

                _cards[i].Init(_matchGameSettings.Data[dataIndex].Sprite, _matchGameSettings.Data[dataIndex].Id);

                if (i % 2 != 0)
                {
                    dataIndex++;
                }
            }
            
            ShuffleCards();
        }

        private void ShuffleCards()
        {
            for (int i = 0; i < _cards.Count; i++)
            {
                MatchCard temp = _cards[i];
                int randomIndex = Random.Range(i, _cards.Count);
                _cards[i] = _cards[randomIndex];
                _cards[randomIndex] = temp;
            }

            for (int i = 0; i < _cards.Count; i++)
            {
                _cards[i].transform.SetSiblingIndex(i);
            }
        }
    }
}