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
        private float _offsetTime = 0;
        
        private bool _runTimer = false;

        private void Update()
        {
            if (_runTimer == false) return;
            
            RunTimer();
        }

        private void RunTimer()
        {
            _startTime = Time.time - _offsetTime;
            _milliseconds = (int)(_startTime * 100) % 100;
            _seconds = (int)_startTime % 60;
            _minutes = (int)_startTime / 60;

            SetTimerTxt();
        }

        private void SetTimerTxt()
        {
            _timerTxt.text = string.Format("{0:00}:{1:00}:{2:00}", _minutes, _seconds, _milliseconds);
        }
        
        public void StartTimer()
        {
            _offsetTime = Time.time;
            _runTimer = true;
        }

        public void StopTimer()
        {
            _runTimer = false;
            GameManager.Instance.UIManager.AddTimeTxt(_timerTxt.text);
        }

        public void ResetTimer()
        {
            _milliseconds = 0;
            _seconds = 0;
            _minutes = 0;
            _startTime = 0;
            
            SetTimerTxt();
        }
    }
}
