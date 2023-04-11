using Assets.Game.Scripts.Services;
using Assets.Game.Scripts.Services.GameObjectKeeperService;
using System;
using UnityEngine;

namespace Assets.Game.Scripts.Entity.BaseEntityScripts
{
    public class BaseEntity : MonoBehaviour, IDamageTaker
    {
        public virtual event Action TakeDamageEvent;

        public virtual event Action DeathEvent;

        private float _speed;
        private float _maxHp;
        private float _currentHP;
        private GameObject _model;
        private bool _isDead;
        protected IGameObjectKeeperService _gameObjectKeeperService;

        public GameObject Model { get => _model; }
        public float Speed { get => _speed; set => _speed = value; }
        public float MaxHp { get => _maxHp; set => _maxHp = value; }
        public float CurrentHP { get => _currentHP; set => _currentHP = value; }

        private void Awake()
        {
            _gameObjectKeeperService = AllServices.Container.Single<IGameObjectKeeperService>();
        }

        public void SetModel(GameObject model)
        {
            if (model != null)
                Destroy(Model);

            _model = Instantiate(model, transform.position, Quaternion.identity, transform);

            CurrentHP = _maxHp;
        }

        public virtual void TakeDamage(float damage)
        {
            if (_isDead) return;

            CurrentHP -= damage;
            TakeDamageEvent?.Invoke();

            if (CurrentHP <= 0)
            {
                _isDead = true;
                DeathEvent?.Invoke();

                Destroy(gameObject, 3f);
            }
        }
    }
}