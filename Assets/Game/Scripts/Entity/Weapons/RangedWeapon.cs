using Assets.Game.Scripts.Entity.Bullet;
using Assets.Game.Scripts.Entity.Weapons;
using Assets.Game.Scripts.Infrastructure.GameFactory;
using Assets.Game.Scripts.Services;
using Assets.Game.Scripts.Services.PoolService;
using DG.Tweening;
using UnityEngine;

public class RangedWeapon : Weapon
{
    public bool CanShoot = true;

    private IGameFactory _factory;

    private ProjectileType _bulletType;
    private float _rateOfFire;
    private Transform _burrel;


    public float RateOfFire { get => _rateOfFire; set => _rateOfFire = value; }
    public ProjectileType BulletType { get => _bulletType; set => _bulletType = value; }
    public Transform Burrel { get => _burrel; set => _burrel = value; }

    private void Awake()
    {
        _factory = AllServices.Container.Single<IGameFactory>();
    }

    public void Shoot()
    {
        if (!CanShoot) return;

        CanShoot = false;
        
        UseBullet();
        DOTween.Sequence().
            AppendInterval(RateOfFire).
            OnComplete(() =>
            {
                CanShoot = true;
                UseBullet();
            });
    }

    private void UseBullet()
    {
        // var bullet = _pullService.Pool.Get();
        var bullet = _factory.CreateBullet(_burrel.transform.position, _bulletType);
        bullet.transform.SetPositionAndRotation(_burrel.transform.position, _burrel.transform.rotation.normalized);
        //DOTween.Sequence().AppendInterval(_bullet.LifeTime).OnComplete(() => Destroy(bullet));
    }
}