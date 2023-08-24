using System;
using System.Collections;
using UnityEngine;

namespace CMG.BallMazeGame
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        public bool GameOver { get; private set; }
        public UIManager UIManager { get; private set; }

        [SerializeField] private GameObject _menuCamera;
        [SerializeField] private GameObject _gameCamera;
        
        [SerializeField] private Board _board;
        [SerializeField] private Ball _ball;
        
        [SerializeField] private int _lives = 3;

        private GameState _gameState = GameState.Start;
        
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

            UIManager = GetComponent<UIManager>();
            
            _ball.ResetEvent += ResetGamePosition;
            _ball.FinishEvent += OnFinishEvent;
            
            UIManager.StartTimer();
        }

        private void Update()
        {
            if (_gameState == GameState.Start)
            {
                if (Input.anyKeyDown)
                {
                    Debug.Log("Starting game");
                    ChangeState(GameState.GameRunning);
                }
            }
        }

        private void OnFinishEvent()
        {
            ChangeState(GameState.GameOver);
        }

        public void ResetGame()
        {
        }

        public void ResetGamePosition()
        {
            if (GameOver == true) return;
            
            StartCoroutine(ResetGamePositionsRoutine());
        }

        private IEnumerator ResetGamePositionsRoutine()
        {
            yield return new WaitForSeconds(.5f);
            ResetBoard();
            yield return new WaitForSeconds(.2f);
            ResetBall();
        }

        public void HandleInput(float[] data)
        {
            _board.HandleInput(data);
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
            UIManager.UpdateLivesUI(_lives);
        }

        private void ChangeState(GameState newState)
        {
            switch (newState)
            {
                case GameState.Start:
                    break;
                case GameState.GameRunning:
                    GameOver = false;
                    UIManager.HandleGameScreen(true);
                    UIManager.HandleStartScreen(false);
                    break;
                case GameState.GameOver:
                    GameOver = true;
                    UIManager.HandleGameScreen(true);
                    UIManager.HandleStartScreen(false);
                    break;
            }

            _gameState = newState;
        }
    }

    public enum GameState
    {
        Start,
        GameRunning,
        GameOver
    }
}