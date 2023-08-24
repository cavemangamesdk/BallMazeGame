using CMG.BallMazeGame.Models;
using UnityEngine;

namespace CMG.BallMazeGame
{
    public class Board : MonoBehaviour
    {
        [SerializeField] private float _tiltSpeedModifier = 1;
        [SerializeField] private Transform _innerBoard;
        [SerializeField] private Transform _outerBoard;

        private float _xRot;
        private float _zRot;

        private Vector3 _innerBoardRotation;
        private Vector3 _outerBoardRotation;

        private void Update()
        {
            //HandleInput();
            SetRotation();
        }

        public void HandleInput(float[] data)
        {
            // Debug.Log($"X Raw: {data.pitch}");
            // Debug.Log($"Z Raw: {data.roll}");

            _xRot = data[0];
            _zRot = data[1];
        }
        
        // private void HandleInput()
        // {
        //     _xRot -= Input.GetAxisRaw("Vertical") * Time.deltaTime * _tiltSpeedModifier;
        //     _zRot += Input.GetAxisRaw("Horizontal") * Time.deltaTime * _tiltSpeedModifier;
        //
        //     _xRot = Mathf.Clamp(_xRot, -8, 8);
        //     _zRot = Mathf.Clamp(_zRot, -8, 8);
        // }

        private void SetRotation()
        {
            _xRot = Mathf.Clamp(_xRot, -8.0f, 8.0f);
            _zRot = Mathf.Clamp(_zRot, -8.0f, 8.0f);
            
            _innerBoard.localRotation = Quaternion.Euler(_xRot, 0, 0);
            _outerBoard.localRotation = Quaternion.Euler(0, 0, _zRot);
        }

        public void ResetBoard()
        {
            _xRot = 0;
            _zRot = 0;
        }
    }
}
