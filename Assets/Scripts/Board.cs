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
            HandleInput();
            SetRotation();
        }

        private void HandleInput()
        {
            _xRot -= Input.GetAxisRaw("Vertical") * Time.deltaTime * _tiltSpeedModifier;
            _zRot += Input.GetAxisRaw("Horizontal") * Time.deltaTime * _tiltSpeedModifier;

            _xRot = Mathf.Clamp(_xRot, -8, 8);
            _zRot = Mathf.Clamp(_zRot, -8, 8);
        }

        private void SetRotation()
        {
            _innerBoard.localRotation = Quaternion.Euler(_xRot, 0, 0);
            _outerBoard.localRotation = Quaternion.Euler(0, 0, _zRot);
        }

        public void ResetBoard()
        {
            _xRot = 0;
            _zRot = 0;
        }
        
        //Get data in from the PI.
        //Handle data, so convert, clamp and normalize it if necessary.
        //Set the rotation on the outer and inner board.
    }
}
