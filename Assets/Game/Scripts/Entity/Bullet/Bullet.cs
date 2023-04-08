using Assets.Game.Scripts.Entity.BaseEntityScripts;
using DG.Tweening;
using UnityEngine;

namespace Assets.Game.Scripts.Entity.Bullet
{
    public class Bullet : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        private float _speed;
        private float _damage;
        private LayerMask _targetMask;
        private float _lifeTime;
        private GameObject _model;

        public float Speed { get => _speed; set => _speed = value; }
        public float Damage { get => _damage; set => _damage = value; }
        public LayerMask TargetMask { get => _targetMask; set => _targetMask = value; }
        public float LifeTime { get => _lifeTime; set => _lifeTime = value; }
        public GameObject Model { get => _model; }
        public Rigidbody Rigidbody { get => _rigidbody; set => _rigidbody = value; }

        private Sequence diedSeq;

        private void Start()
        {
            SetModel(Model);
            Flight();
            Model.transform.localRotation = new Quaternion(0, 0, 0, 0);
        }

        public GameObject SetModel(GameObject model)
        {
            if (model != null)
                Destroy(Model);

            return _model = Instantiate(model, transform.position, Quaternion.identity, transform);
        }

        private void Flight()
        {
            diedSeq = DOTween.Sequence();
            Rigidbody.AddForce(transform.forward * _speed, ForceMode.Impulse);
            diedSeq.AppendInterval(3f).OnComplete(() => { Destroy(gameObject); });
        }

        private void OnCollisionEnter(Collision collision)
        {
            Hit(collision);
        }

        private void Hit(Collision collision)
        {
            if ((_targetMask & (1 << collision.gameObject.layer)) != 0)
            {
                if (collision.gameObject.TryGetComponent(out IDamageTaker damageTaker))
                {
                    damageTaker.TakeDamage(_damage);
                }

                diedSeq.Kill();
                Destroy(gameObject);
            }

            Destroy(gameObject);
            diedSeq.Kill();
        }
    }
}