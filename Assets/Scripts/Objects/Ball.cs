using System;
using UnityEngine;

namespace CMG.BallMazeGame
{
    public class Ball : MonoBehaviour
    {
        public event Action ResetEvent;
        public event Action FinishEvent;

        public event Action HoleEvent;

        [SerializeField] private Transform _ballStartPosition;
        [SerializeField] private Rigidbody _rigidbody;

        [SerializeField] private float _correctionRayRange = 2;

        private Vector3 _boundsSize;

        private bool _isDead = false;
        private bool _checkForCorrection = true;
        
        private void Start()
        {
            _boundsSize = GetComponent<Collider>().bounds.size;
        }
        
        private void OnCollisionEnter(Collision collision)
        {
            if (_isDead == true) return;
            
            if (collision.gameObject.CompareTag("SubtractLife"))
            {
                ResetEvent?.Invoke();
                GameManager.Instance.SubtractLife();
                _isDead = true;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log($"Ball hit {other.name} trigger");
            
            if (other.CompareTag("Hole"))
            {
                //Disable raycasting until we reset ball...
                //Debug.Log("We hit a hole!");
                _checkForCorrection = false;
                HoleEvent?.Invoke();
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
            _isDead = false;
            _checkForCorrection = true;
        }

        
        private void Update()
        {
            if (_checkForCorrection == false) return;

            RaycastHit hit;
            if (Physics.Raycast(transform.position,transform.InverseTransformDirection(transform.up),out hit,_correctionRayRange))
            {
                //Debug.Log(hit.collider.name);
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
