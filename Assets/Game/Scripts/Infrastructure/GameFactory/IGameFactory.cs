using Assets.Game.Scripts.Entity;
using Assets.Game.Scripts.Entity.Bullet;
using Assets.Game.Scripts.Entity.Enemy;
using Assets.Game.Scripts.Entity.Turret;
using Assets.Game.Scripts.Services;
using UnityEngine;

namespace Assets.Game.Scripts.Infrastructure.GameFactory
{
    public interface IGameFactory : IService
    {
        GameObject CreateHero(GameObject playerSpawnPoint);
        Turret CreateTurret(Vector3 playerSpawnPoint, TurretType _turretType);
        Bullet CreateBullet(Vector3 spawnPosition, ProjectileType bulletType);
        Enemy CreateEnemy(Vector3 spawnPosition, EnemyType weaponType);
        GameObject CreateStartPopup();
        void CreateOverlayPopup();
        GameObject CreateStagePopup();
        GameObject CreateVictoryPopup();
        Laser CreateLaser(Vector3 spawnPosition, ProjectileType projectileType);
    }
}