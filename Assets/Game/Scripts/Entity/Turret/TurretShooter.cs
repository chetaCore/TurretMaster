using Assets.Game.Scripts.Infrastructure.GameFactory;
using Assets.Game.Scripts.Services;
using Assets.Game.Scripts.Services.PoolService;
using DG.Tweening;
using System.Threading;
using UnityEngine;

namespace Assets.Game.Scripts.Entity.Character
{
    public class TurretShooter : MonoBehaviour
    {
        public bool CanShoot;

        private IPoolService _pullService;
        protected IGameFactory _factory;

        protected ProjectileType _projectileType;
        protected float _rateOfFire;
        protected Transform _burrel;

        private bool _shooting;

        public float RateOfFire { get => _rateOfFire; set => _rateOfFire = value; }
        public ProjectileType BulletType { get => _projectileType; set => _projectileType = value; }
        public Transform Burrel { get => _burrel; set => _burrel = value; }

        private void Awake()
        {
            _pullService = AllServices.Container.Single<IPoolService>();
            _factory = AllServices.Container.Single<IGameFactory>();
        }


        private void Update()
        {
            if (!CanShoot) return;
            if (!_shooting)
                Shoot();
        }

        private void Shoot()
        {
            _shooting = true;
            UseProjectile();
            DOTween.Sequence().AppendInterval(_rateOfFire).OnComplete(() => _shooting = false);
        }

        protected virtual void UseProjectile()
        {
            // var bullet = _pullService.Pool.Get();
            var bullet = _factory.CreateBullet(_burrel.transform.position, _projectileType);
            bullet.transform.SetPositionAndRotation(_burrel.transform.position, _burrel.rotation);
            //DOTween.Sequence().AppendInterval(_bullet.LifeTime).OnComplete(() => Destroy(bullet));
        }
    }
}