using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CMG.BallMazeGame.Data
{
    public class DataCollector : MonoBehaviour
    {
        private Transform _ballPos;
        private Transform _outerboardRot;
        private Transform _innerBoardRot;
        private Board _boardInput;

        [SerializeField] private List<Vector3> _lostLifePositions = new List<Vector3>();
        [HideInInspector] private GameData[] _gameDataArray = new GameData[200000];
        private int _index = 0;
        
        private void Awake()
        {
            _ballPos = GameObject.FindWithTag("Ball").GetComponent<Transform>();
            _innerBoardRot = GameObject.FindWithTag("InnerBoard").GetComponent<Transform>();
            _outerboardRot = GameObject.FindWithTag("OuterBoard").GetComponent<Transform>();
            _boardInput = GameObject.FindWithTag("Board").GetComponent<Board>();
            
            _ballPos.GetComponent<Ball>().HoleEvent += OnHoleEvent;
        }

        private void OnHoleEvent()
        {
            _lostLifePositions.Add(_ballPos.position);
        }

        private void Start()
        {
            GameManager.Instance.UIManager.SubmitEvent += OnSubmitEvent;
        }

        private void OnSubmitEvent(PlayerData playerData)
        {
            var array = _gameDataArray.Where(b => b.BallPosition != Vector3.zero).ToArray();
            
            var sessionData = new SessionData()
            {
                Guid = Guid.NewGuid().ToString(),
                GameData = array,
                PlayerData = playerData,
                LostLifePosition = _lostLifePositions.ToArray()
            };
            
            NetworkDataHandler.Instance.HandleWebrequest(sessionData);

            _index = 0;
            _gameDataArray = new GameData[200000];
        }

        private void Update()
        {
            if (GameManager.Instance.GameState != GameState.GameRunning) return;

            var data = new GameData()
            {
                TimeStamp = DateTime.Now,
                BallPosition = _ballPos.position,
                BoardRotation = new Vector3(_innerBoardRot.rotation.eulerAngles.x,0,_outerboardRot.rotation.eulerAngles.z),
                InputData = _boardInput.BoardInput
            };
            
            _gameDataArray[_index++] = data;
        }
    }

    [Serializable]
    public struct GameData
    {
        public DateTime TimeStamp;
        public Vector3 BallPosition;
        public Vector3 BoardRotation;
        public Vector2 InputData;
    }
    
    [Serializable]
    public struct PlayerData
    {
        public string Name;
        public int Lives;
        public string GameTime;
    }

    [Serializable]
    public struct SessionData
    {
        public string Guid;
        public PlayerData PlayerData;
        public GameData[] GameData;
        public Vector3[] LostLifePosition;
    }
}