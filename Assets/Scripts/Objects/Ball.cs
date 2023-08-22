using System;
using UnityEngine;

namespace CMG.BallMazeGame
{
    public class Ball : MonoBehaviour
    {
        public event Action ResetEvent;
        public event Action FinishEvent;

        [SerializeField] private Transform _ballStartPosition;
        [SerializeField] private Rigidbody _rigidbody;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("SubtractLife"))
            {
                GameManager.Instance.SubtractLife();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("ResetZone"))
            {
                ResetEvent?.Invoke();
            }

            if (other.CompareTag("FinishZone"))
            {
                FinishEvent?.Invoke();
            }
        }

        public void ResetPosition()
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.Sleep();
            transform.position = _ballStartPosition.position;
        }
    }
}
