using CMG.BallMazeGame.Models;
using UnityEngine;

namespace CMG.BallMazeGame
{
    public class Cylinder : MonoBehaviour
    {
        [SerializeField] private Vector3 Rot;
        [SerializeField] private Vector3 Acc;
        [SerializeField] private Vector3 Pos;
        [SerializeField] private Vector3 Vel;
        [SerializeField] private int test;

        private void FixedUpdate()
        {
            //HandleInput();
            SetRotation();
        }

        public void HandleInput(float[] data)
        {
            // Debug.Log($"X Raw: {data.pitch}");
            // Debug.Log($"Z Raw: {data.roll}");
            test += 1;
            
            Rot = new Vector3(data[0], data[1], data[2]);
            Acc = new Vector3(data[3], data[4], data[5]);
            Vel += Acc * 0.02f;
            Pos += Vel * 0.02f;
            
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
            //_yRot = Mathf.Clamp(_yRot, -10.0f, 10.0f);
            //_zRot = Mathf.Clamp(_zRot, -10.0f, 10.0f);

            //_innerBoard.localRotation = Quaternion.Euler(_xRot, 0, 0);
            //_outerBoard.localRotation = Quaternion.Euler(0, 0, _zRot);

            transform.localRotation = Quaternion.Euler(Rot);
            //transform.position = Pos;
        }

        public void ResetBoard()
        {
            Rot = Vector3.zero;
        }
    }
}
