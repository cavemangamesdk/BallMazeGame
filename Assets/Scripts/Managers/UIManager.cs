using System;
using CMG.BallMazeGame.Data;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace CMG.BallMazeGame
{
    public class UIManager : MonoBehaviour
    {
        public event Action<PlayerData> SubmitEvent;

        [SerializeField] private GameTimer _gameTimer;

        [SerializeField] private GameObject _lastLife;
        [SerializeField] private GameObject _secondLife;
        [SerializeField] private GameObject _thirdLife;

        [SerializeField] private GameObject _startScreen;
        [SerializeField] private GameObject _gameScreen;
        [SerializeField] private GameObject _gameOverScreen;

        [SerializeField] private TextMeshProUGUI _timeTxt;
        [SerializeField] private TMP_InputField _nameInputField;
        [SerializeField] private Button _submitButton;

        private void Awake()
        {
            _submitButton.onClick.AddListener(SubmitPlayerData);
            _nameInputField.onValidateInput += delegate(string s, int i, char c)
            {
                if (s.Length >= 12)
                {
                    return '\0';
                }

                return char.ToUpper(c);
            };
        }

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
            if (lives == 3)
            {
                _thirdLife.SetActive(true);
                _secondLife.SetActive(true);
                _lastLife.SetActive(true);
            }
            else if (lives == 2)
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

        public void HandleGameUI(GameState state)
        {
            switch (state)
            {
                case GameState.Start:
                    HandleGameScreen(false);
                    HandleGameOverScreen(false);
                    HandleStartScreen(true);
                    break;
                case GameState.GameRunning:
                    HandleGameScreen(true);
                    HandleGameOverScreen(false);
                    HandleStartScreen(false);
                    break;
                case GameState.GameOver:
                    HandleGameScreen(false);
                    HandleGameOverScreen(true);
                    HandleStartScreen(false);
                    break;
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

        public void HandleGameOverScreen(bool show)
        {
            _gameOverScreen.SetActive(show);
            _nameInputField.text = "";
        }

        public void AddTimeTxt(string time)
        {
            _timeTxt.text = time;
        }

        private void SubmitPlayerData()
        {
            var result = GameManager.Instance.Lives > 0
                ? $"won the game with {GameManager.Instance.Lives} lives left"
                : "lost the game";

            Debug.Log($"Player: {_nameInputField.text} has {result}, with a time of {_timeTxt.text}");
            
            var data = new PlayerData()
            {
                Name = _nameInputField.text,
                Lives = GameManager.Instance.Lives,
                GameTime = _timeTxt.text
            };
            
            SubmitEvent?.Invoke(data);
        }
    }
}