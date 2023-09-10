using System.Collections;
using Cinemachine;
using CMG.BallMazeGame.Data;
using UnityEngine;

namespace CMG.BallMazeGame
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        public UIManager UIManager { get; private set; }
        public int Lives { get => _lives; }
        
        [SerializeField] private GameObject _menuCamera;
        [SerializeField] private GameObject _gameCamera;
        [SerializeField] private CinemachineVirtualCamera _virtualCamera;
        
        [SerializeField] private Board _board;
        [SerializeField] private Ball _ball;
        [SerializeField] private MotionControllerServer _motionController;
        
        [SerializeField] private int _lives = 3;

        [HideInInspector] public GameState GameState { get; private set; }
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

            GameState = GameState.Start;
            
            UIManager = GetComponent<UIManager>();

            UIManager.SubmitEvent += ResetGame;
            
            _ball.ResetEvent += OnResetGamePosition;
            _ball.FinishEvent += OnFinishEvent;
            _virtualCamera = _gameCamera.GetComponent<CinemachineVirtualCamera>();
            _motionController.OnJoystickPressed += OnJoystickPressed;
            
            ChangeState(GameState.Start);
        }

        private void Update()
        {
            if (GameState == GameState.Start)
            {
                if (Input.anyKeyDown)
                { 
                    ChangeState(GameState.GameRunning);
                }

                if (_motionController.JoystickState == "pressed")
                {
                    OnJoystickPressed();
                }
            }
        }

        public void OnJoystickPressed()
        {
            if (GameState == GameState.Start)
                ChangeState(GameState.GameRunning);

            if (GameState == GameState.GameOver)
            {
                Debug.Log("Sending data!");
                UIManager.SubmitButton.onClick?.Invoke();
            }
        }
        
        private void OnFinishEvent()
        {
            ChangeState(GameState.GameOver);
        }
        
        public void ResetGame(PlayerData playerData)
        {
            ChangeState(GameState.Start);
        }
        
        public void OnResetGamePosition()
        {
            if (_resetRoutine != null)
                StopCoroutine(_resetRoutine);
            
            //Debug.Log("Resetting");
            
            _resetRoutine = StartCoroutine(ResetGamePositionsRoutine());
        }

        private IEnumerator ResetGamePositionsRoutine()
        {
            _virtualCamera.Follow = null;
            yield return new WaitForSeconds(.5f);
            ResetBoard();
            yield return new WaitForSeconds(.2f);
            ResetBall();
            _virtualCamera.Follow = _ball.transform;
        }

        public void HandleInput(float[] data)
        {
            //Debug.Log($"x: {data[0]}, z: {data[1]}");
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
                    _lives = 3;
                    UIManager.UpdateLivesUI(_lives);
                    UIManager.StopTimer();
                    UIManager.ResetTimer();
                    UIManager.HandleGameUI(newState);
                    break;
                case GameState.GameRunning:
                    _menuCamera.SetActive(false);
                    _gameCamera.SetActive(true);
                    UIManager.ResetTimer();
                    UIManager.StartTimer();
                    UIManager.HandleGameUI(newState);
                    break;
                case GameState.GameOver:
                    _menuCamera.SetActive(true);
                    _gameCamera.SetActive(false);
                    OnResetGamePosition();
                    UIManager.StopTimer();
                    UIManager.HandleGameUI(newState);
                    break;
            }

            GameState = newState;
        }
    }

    public enum GameState
    {
        Start,
        GameRunning,
        GameOver
    }
}