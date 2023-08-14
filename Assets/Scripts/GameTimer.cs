using System;
using TMPro;
using UnityEngine;

namespace CMG.BallMazeGame
{
    public class GameTimer : MonoBehaviour
    {
        [SerializeField] private TMP_Text _timerTxt;
        private int _milliseconds = 0;
        private int _seconds = 0;
        private int _minutes = 0;
        private float _startTime = 0;

        private bool _runTimer;
        
        private void Update()
        {
            RunTimer();
        }

        private void RunTimer()
        {
            _startTime = Time.time;
            _milliseconds = (int)(_startTime * 100) % 100;
            _seconds = (int)_startTime % 60;
            _minutes = (int)_startTime / 60;

            _timerTxt.text = string.Format("{0:00}:{1:00}:{2:00}", _minutes, _seconds, _milliseconds);
        }

        private void StartTimer()
        {
            
        }

        private void StopTimer()
        {
            
        }
    }
}
