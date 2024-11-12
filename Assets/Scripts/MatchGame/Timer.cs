using System;
using UnityEngine;

namespace MatchGame
{
    public class Timer : MonoBehaviour
    {
        public float Value { get; private set; }
        
        private void Update()
        {
            Value += Time.deltaTime;
        }
    }
}