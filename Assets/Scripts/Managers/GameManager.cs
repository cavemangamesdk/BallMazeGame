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
            
            _ball.ResetEvent += ResetGame;
            _ball.FinishEvent += OnFinishEvent;
        }

        private void OnFinishEvent()
        {
            GameOver = true;
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
    }
}