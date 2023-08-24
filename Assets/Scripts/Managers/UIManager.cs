using Newtonsoft.Json.Bson;
using UnityEngine;

namespace CMG.BallMazeGame
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameTimer _gameTimer;

        [SerializeField] private GameObject _lastLife;
        [SerializeField] private GameObject _secondLife;
        [SerializeField] private GameObject _thirdLife;

        [SerializeField] private GameObject _startScreen;
        [SerializeField] private GameObject _gameScreen;
        
        public void StartTimer()
        {
            _gameTimer.StartTimer();
        }

        public void StopTimer()
        {
            _gameTimer.StopTimer();
        }

        public void ResetTimer()
        {
            _gameTimer.ResetTimer();
        }
        
        public void UpdateLivesUI(int lives)
        {
            if (lives == 2)
            {
                _thirdLife.SetActive(false);
            }
            else if (lives == 1)
            {
                _secondLife.SetActive(false);
            }
            else if (lives == 0)
            {
                _lastLife.SetActive(false);
            }
        }

        public void HandleGameScreen(bool show)
        {
            _gameScreen.SetActive(show);   
        }
        
        public void HandleStartScreen(bool show)
        {
            _startScreen.SetActive(show);
        }
    }
}