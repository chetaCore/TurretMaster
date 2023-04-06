using UnityEngine;

namespace Assets.Game.Scripts.Entity.BaseEntityScripts
{
    public class EntityDescent : MonoBehaviour
    {
        [SerializeField] private float _descentSpeed;

        public bool CanDescent;

        private void Update()
        {
            if (!CanDescent) return;
            Descent();
        }

        private void Descent()
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - _descentSpeed * Time.deltaTime, transform.position.z);
        }
    }
}