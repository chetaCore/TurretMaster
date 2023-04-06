using TMPro;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Game.Scripts.Entity.Enemy
{
    public class EnemyMover : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _meshAgent;

        private Transform _target;
        private float _maxDistance = 100f;
        private float _offset = 0.6f;
        private Vector3 destination;

        public bool CanMove;
        private Vector3 lastKnowPosition;

        public Transform Target { get => _target; set => _target = value; }
        public NavMeshAgent MeshAgent { get => _meshAgent;}

        private void Update()
        {
            if (!CanMove)
            {
                if(MeshAgent.enabled)
                    MeshAgent.SetDestination(transform.position);
                return;
            }

            if (Target == null)
            {
                if (MeshAgent.remainingDistance < 0.5f)
                {
                    SetRandomDestination();
                }
            }
            else if(_target.position != lastKnowPosition)
            {
                lastKnowPosition = _target.position;
                Vector3 targetDirection = (lastKnowPosition - transform.position).normalized;
                Vector3 targetPosition = lastKnowPosition - targetDirection * _offset;
                MeshAgent.SetDestination(targetPosition);
            }
        }

        private void SetRandomDestination()
        {
            Vector3 randomDirection = Random.insideUnitSphere * _maxDistance;
            randomDirection += transform.position;
            NavMeshHit navMeshHit;
            NavMesh.SamplePosition(randomDirection, out navMeshHit, _maxDistance, NavMesh.AllAreas);
            destination = navMeshHit.position;
            MeshAgent.SetDestination(destination);
        }
    }
}