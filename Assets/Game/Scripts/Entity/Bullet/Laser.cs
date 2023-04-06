using Assets.Game.Scripts.Entity.BaseEntityScripts;
using UnityEngine;

namespace Assets.Game.Scripts.Entity.Bullet
{
    public class Laser : MonoBehaviour
    {
        private LayerMask _targetMask;
        private LineRenderer _lineRenderer;
        private float _width = 0.1f;
        private float _length = 50f;
        private Color _startColor = Color.blue;
        private Color _endColor = Color.red;
        private float _damage = 10f;

        public float Width { get => _width; set => _width = value; }
        public float Length { get => _length; set => _length = value; }
        public float Damage { get => _damage; set => _damage = value; }
        public LayerMask TargetMask { get => _targetMask; set => _targetMask = value; }
        public LineRenderer LineRenderer { get => _lineRenderer; set => _lineRenderer = value; }

        private void Start()
        {
            LineRenderer.startColor = _startColor;
            LineRenderer.endColor = _endColor;
            _lineRenderer.enabled = false;
        }

        private void Update()
        {
            LineRenderer.SetPosition(0, transform.position);
            LineRenderer.SetPosition(1, transform.position + transform.forward * _length);

            LineRenderer.startWidth = Width;
            LineRenderer.endWidth = Width;
            LineRenderer.startColor = _startColor;
            LineRenderer.endColor = _endColor;

            RaycastHit[] hits = Physics.RaycastAll(transform.position, transform.position + transform.forward * _length - transform.position, Length, TargetMask);
            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.TryGetComponent(out IDamageTaker damgeTaker))
                {
                    damgeTaker.TakeDamage(Damage);
                }
            }
        }
    }
}