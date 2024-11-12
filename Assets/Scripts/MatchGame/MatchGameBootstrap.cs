using System;
using UnityEngine;

namespace MatchGame
{
    public class MatchGameBootstrap : MonoBehaviour
    {
        [SerializeField] private GameBoard _gameBoard;
        [SerializeField] private BoardController _boardController;
        [SerializeField] private GameStateHandler _gameStateHandler;
        [SerializeField] private Timer _timer;
        
        private void Start()
        {
            _gameBoard.Init();
            _boardController.Init(_gameBoard.Cards);
            _gameStateHandler.Init(_boardController, _timer);
        }
    }
}