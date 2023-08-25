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
        public int Lives { get => _lives; }
        
        [SerializeField] private GameObject _menuCamera;
        [SerializeField] private GameObject _gameCamera;
        
        [SerializeField] private Board _board;
        [SerializeField] private Ball _ball;
        
        [SerializeField] private int _lives = 3;

        private GameState _gameState = GameState.Start;
        private Coroutine _resetRoutine;
        
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

            UIManager.SubmitEvent += ResetGame;
            
            _ball.ResetEvent += ResetGamePosition;
            _ball.FinishEvent += OnFinishEvent;
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
            ChangeState(GameState.Start);
        }
        
        public void ResetGamePosition()
        {
            if (_resetRoutine != null)
                StopCoroutine(_resetRoutine);
            
            _resetRoutine = StartCoroutine(ResetGamePositionsRoutine());
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

            if (_lives <= 0)
                ChangeState(GameState.GameOver);
        }

        private void ChangeState(GameState newState)
        {
            switch (newState)
            {
                case GameState.Start:
                    GameOver = true;
                    _lives = 3;
                    UIManager.UpdateLivesUI(_lives);
                    UIManager.StopTimer();
                    UIManager.ResetTimer();
                    UIManager.HandleGameUI(newState);
                    break;
                case GameState.GameRunning:
                    GameOver = false;
                    UIManager.ResetTimer();
                    UIManager.StartTimer();
                    UIManager.HandleGameUI(newState);
                    break;
                case GameState.GameOver:
                    GameOver = true;
                    ResetGamePosition();
                    UIManager.StopTimer();
                    UIManager.HandleGameUI(newState);
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