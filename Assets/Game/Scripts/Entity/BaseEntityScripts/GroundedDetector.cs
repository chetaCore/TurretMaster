using UnityEngine;

namespace Assets.Game.Scripts.Entity.BaseEntityScripts
{
    public class GroundedDetector : MonoBehaviour
    {
        [SerializeField] private LayerMask _groundMask;
        [SerializeField] private Transform _rayOrigin;

        private GameObject _entity;
        private readonly float _raycastDistance = 1f;

        public bool CanDetected;
        public bool EntityOnGround;

        public GameObject Entity { get => _entity; set => _entity = value; }

        private void Update()
        {
            if (!CanDetected) return;

            if (Physics.Raycast(_entity.transform.position - Vector3.up, -Vector3.up, _raycastDistance, _groundMask))
            {
                EntityOnGround = true;
                CanDetected = false;
            }

            Debug.DrawRay(_rayOrigin.position, -Vector3.up, Color.red, _raycastDistance);
        }
    }
}