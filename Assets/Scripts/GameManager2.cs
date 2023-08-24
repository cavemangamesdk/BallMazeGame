using System;
using System.Collections;
using System.Collections.Generic;
using CMG.BallMazeGame.Models;
using UnityEngine;

namespace CMG.BallMazeGame
{
    public class GameManager2 : MonoBehaviour
    {
        public static GameManager2 Instance;
        
        [SerializeField] private Cylinder _cylinder;
 

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

            //_ball.ResetEvent += ResetGame;
        }

        public void ResetGame()
        {
            StartCoroutine(ResetGamePositions());
        }

        private IEnumerator ResetGamePositions()
        {
            yield return new WaitForSeconds(.5f);
            ResetBoard();
            yield return new WaitForSeconds(.2f);
            ResetBall();
        }

        public void HandleInput(float[] data)
        {
            _cylinder.HandleInput(data);
        }
        
        private void ResetBoard()
        {
            _cylinder.ResetBoard();
        }
        
        private void ResetBall()
        {
            //_ball.ResetPosition();
        }

        public void SubtractLife()
        {
            //_lives--;
            //UpdateLivesUI();
        }

        private void UpdateLivesUI()
        {
            //if (_lives == 2)
            //{
            //    _thirdLife.SetActive(false);
            //}
            //else if (_lives == 1)
            //{
            //    _secondLife.SetActive(false);
            //}
            //else if (_lives == 0)
            //{
            //    _lastLife.SetActive(false);
            //}
        }
    }
}