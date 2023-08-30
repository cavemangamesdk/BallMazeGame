using System;
using UnityEngine;

namespace CMG.BallMazeGame
{
    public class Ball : MonoBehaviour
    {
        public event Action ResetEvent;
        public event Action FinishEvent;

        public event Action LostLife;

        [SerializeField] private Transform _ballStartPosition;
        [SerializeField] private Rigidbody _rigidbody;

        [SerializeField] private float _correctionRayRange = 2;

        private Vector3 _boundsSize;

        private void Start()
        {
            _boundsSize = GetComponent<Collider>().bounds.size;
        }
        
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("SubtractLife"))
            {
                LostLife?.Invoke();
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

        
        private void Update()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position,transform.InverseTransformDirection(transform.up),out hit,_correctionRayRange))
            {
                Debug.Log(hit.collider.name);
                transform.position = hit.point + new Vector3(0, _boundsSize.y, 0);
            }   
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position,transform.InverseTransformDirection(transform.up*_correctionRayRange));
        }
    }
}
