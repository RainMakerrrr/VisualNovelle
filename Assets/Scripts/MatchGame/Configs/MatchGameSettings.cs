using System;
using UnityEngine;

namespace MatchGame.Configs
{
    [CreateAssetMenu(menuName = "Configs/Match Game Settings", fileName = "MatchGameSettings")]
    public class MatchGameSettings : ScriptableObject
    {
        [SerializeField] private Vector2Int _boardSize;
        [SerializeField] private MatchCardData[] _data;

        public int BoardSize => _boardSize.x * _boardSize.y;

        public MatchCardData[] Data => _data;
    }

    [Serializable]
    public class MatchCardData
    {
        public string Id;
        public Sprite Sprite;
    }
}