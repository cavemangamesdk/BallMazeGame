using System;
using System.Collections;
using UnityEngine;

namespace CMG.BallMazeGame
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        
        [SerializeField] private Board _board;
        [SerializeField] private Ball _ball;

        [SerializeField] private GameObject _lastLife;
        [SerializeField] private GameObject _secondLife;
        [SerializeField] private GameObject _thirdLife;

        [SerializeField] private int _lives = 3;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            if (Instance != this)
            {
                Destroy(this);
            }

            _ball.ResetEvent += ResetGame;
        }

        public void ResetGame()
        {
            StartCoroutine(ResetGamePositions());
        }
        
        private IEnumerator ResetGamePositions()
        {
            yield return new WaitForSeconds(.5f);
            ResetBoard();
            yield return new WaitForSeconds(.5f);
            ResetBall();
        }

        private void ResetBoard()
        {
            _board.ResetBoard();
        }
        
        private void ResetBall()
        {
            _ball.ResetPosition();
        }

        public void SubtractLife()
        {
            _lives--;
            UpdateLivesUI();
        }

        private void UpdateLivesUI()
        {
            if (_lives == 2)
            {
                _thirdLife.SetActive(false);
            }
            else if (_lives == 1)
            {
                _secondLife.SetActive(false);
            }
            else if (_lives == 0)
            {
                _lastLife.SetActive(false);
            }
        }
    }
}