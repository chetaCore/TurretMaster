using UnityEngine;

namespace Assets.Game.Scripts.Entity.BaseEntityScripts
{
    public class BaseAimer : MonoBehaviour
    {
        private Transform _target;

        public bool CanAim;
        private float _timeToTurn;

        public Transform Target { get => _target; set => _target = value; }
        public float TimeToTurn { get => _timeToTurn; set => _timeToTurn = value; }

        private void Update()
        {
            if (!CanAim) return;
            if (Target == null) return;
            LookAtTarget();
        }

        public void LookAtTarget()
        {
            var heading = _target.position - transform.position;
            var newRotation = Quaternion.LookRotation(heading, Vector3.up).eulerAngles;

            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                Quaternion.Euler(newRotation),
                _timeToTurn * Time.deltaTime);
        }
    }
}