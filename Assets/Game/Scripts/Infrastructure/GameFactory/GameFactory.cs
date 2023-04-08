using Assets.Game.Scripts.Entity.BaseEntityScripts;
using Assets.Game.Scripts.Entity.Bullet;
using Assets.Game.Scripts.Entity.Character;
using Assets.Game.Scripts.Entity.Enemy;
using Assets.Game.Scripts.Entity.Turret;
using Assets.Game.Scripts.Entity.Weapons;
using Assets.Game.Scripts.State.CharacterState;
using Assets.Game.Scripts.State.EnemyState;
using Assets.Game.Scripts.State.EnemyState.RangedEnemyState;
using Assets.Game.Scripts.State.TurretState;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Game.Scripts.Infrastructure.GameFactory
{
    public partial class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assets;
        private GameFactorySettings _settings;

        private GameObject _enemyParentObject;

        public GameFactory(IAssetProvider assets)
        {
            _assets = assets;
            _settings = _assets.GetScriptObject(Constans.GameFactorySettingsPath) as GameFactorySettings;
        }

        public GameObject CreateHero(GameObject playerSpawnPoint)
        {
            PlayerData playerData = _settings.Player;

            if (playerData == null)
                Debug.Log("Player Data = null");

            Player player = _assets.Instantiate(Constans.HeroPath, playerSpawnPoint.transform.position).GetComponent<Player>();

            player.MaxHp = playerData.HP;
            player.Speed = playerData.Speed;
            player.SetModel(playerData.Model);

            var turretInstaller = player.GetComponent<TurrentInstaller>();
            turretInstaller.InstallTime = playerData.InstallTime;

            player.AddComponent<CharacterStateMachine>().Initialize(
                player.GetComponent<Player>(),
                player.GetComponent<CharacterMover>(),
                turretInstaller,
                player.GetComponentInChildren<CharacterAnimator>()
                );

            //Animations

            player.GetComponentInChildren<CharacterAnimator>().CharacterMover =
                player.GetComponent<CharacterMover>();

            //

            return player.gameObject;
        }

        public Turret CreateTurret(Vector3 spawnPosition, TurretType turretType)
        {
            TurretData turretData = null;
            foreach (var turret in _settings.Turrets)
            {
                if (turret.Type == turretType)
                    turretData = turret;
            }

            if (turretData == null)
                Debug.Log("Turret Data = null");

            var turretInstance = _assets.Instantiate(Constans.TurretPath, spawnPosition).GetComponent<Turret>();
            turretInstance.SetModel(turretData.Model);

            var turretObserver = turretInstance.GetComponent<TurretObserver>();
            turretObserver.FieldOfView = turretData.FieldOfView;
            turretObserver.DetectionInterval = turretData.DetectionInterval;
            turretObserver.DetectionMask = turretData.TargetMask;

            TurretShooter turretShooter = null;
            if (turretData.ProjectileType == ProjectileType.TurretDefault)
            {
                turretShooter = turretInstance.GetComponentInChildren<TurretAimer>().AddComponent<TurretShooter>();
            }
            else if (turretData.ProjectileType == ProjectileType.TurretLaser)
            {
                turretShooter = turretInstance.GetComponentInChildren<TurretAimer>().AddComponent<TurretLaserShooter>();
            }

            if (turretShooter == null)
                Debug.Log("TurretProgectileType not correct");

            turretShooter.RateOfFire = turretData.RateOfFire;
            turretShooter.BulletType = turretData.ProjectileType;
            turretShooter.Burrel = turretInstance.GetComponentInChildren<TurretModel>().Burrel;

            var turretAimer = turretInstance.GetComponentInChildren<TurretAimer>();
            turretAimer.TimeToTurn = turretData.TimeToTurn;

            var turretStateMachine = turretInstance.AddComponent<TurretStateMachine>();
            turretStateMachine.Initialize(turretAimer, turretObserver, turretShooter);

            return turretInstance;
        }

        public Bullet CreateBullet(Vector3 spawnPosition, ProjectileType bulletType)
        {
            BulletData Bulletdata = null;
            foreach (var bulletData in _settings.Projectiles)
            {
                if (bulletData.Type == bulletType)
                    Bulletdata = (BulletData)bulletData;
            }

            if (Bulletdata == null)
                Debug.Log("Bullet Data = null");

            Bullet bulletInstance = null;

            if (Bulletdata.Usertype == UserType.Turret)
            {
                bulletInstance = _assets.Instantiate(Constans.ProjectilePath, spawnPosition).AddComponent<Bullet>();
            }
            else if (Bulletdata.Usertype == UserType.Enemy)
            {
                bulletInstance = _assets.Instantiate(Constans.EnemyProjectilePath, spawnPosition).AddComponent<Bullet>();
            }

            bulletInstance.Rigidbody = bulletInstance.AddComponent<Rigidbody>();
            bulletInstance.Rigidbody.useGravity = false;
            bulletInstance.Rigidbody.freezeRotation = true;

            bulletInstance.AddComponent<SphereCollider>();

            bulletInstance.Speed = Bulletdata.Speed;
            bulletInstance.Damage = Bulletdata.Damage;
            bulletInstance.TargetMask = Bulletdata.TargetMask;
            var bulletModel = bulletInstance.SetModel(Bulletdata.Model);
            bulletModel.transform.rotation = new Quaternion(0, 0, 0, 0);
            bulletInstance.LifeTime = Bulletdata.LifeTime;

            return bulletInstance;
        }

        public Laser CreateLaser(Vector3 spawnPosition, ProjectileType projectileType)
        {
            LaserData data = null;
            foreach (var laserData in _settings.Projectiles)
            {
                if (laserData.Type == projectileType)
                    data = (LaserData)laserData;
            }

            if (data == null)
                Debug.Log("Laser Data = null");

            var laserInstance = _assets.Instantiate(Constans.ProjectilePath, spawnPosition).AddComponent<Laser>();

            laserInstance.LineRenderer = laserInstance.AddComponent<LineRenderer>();

            laserInstance.TargetMask |= data.TargetMask;
            laserInstance.Width = data.Width;
            laserInstance.Length = data.Length;
            laserInstance.Damage = data.Damage;

            return laserInstance;
        }

        public Enemy CreateEnemy(Vector3 spawnPosition, EnemyType weaponType)
        {
            EnemyData data = null;
            foreach (var enemyData in _settings.Enemys)
            {
                if (enemyData.Type == weaponType)
                    data = enemyData;
            }

            if (data == null)
                Debug.Log("Enemy Data = null");

            var enemyInstance = _assets.Instantiate(Constans.EnemyPath, spawnPosition).GetComponent<Enemy>();

            //получение компонентов и задание данных

            enemyInstance.Speed = data.Speed;
            enemyInstance.TargetMask = data.TargetMask;
            enemyInstance.MaxHp = data.HP;
            enemyInstance.SetModel(data.Model);

            var enemyObserver = enemyInstance.GetComponent<EnemyObserver>();
            enemyObserver.FieldOfView = data.FieldOfView;
            enemyObserver.DetectionInterval = data.DetectionInterval;
            enemyObserver.DetectionMask = data.TargetMask;

            var enemyAttackTrigger = enemyInstance.GetComponent<EnemyAttackTrigger>();
            enemyAttackTrigger.FieldOfView = data.AttackRange;
            enemyAttackTrigger.DetectionMask = data.TargetMask;

            var enemyMover = enemyInstance.GetComponent<EnemyMover>();

            var enemyAimer = enemyInstance.GetComponent<EnemyAimer>();
            enemyAimer.TimeToTurn = data.TimeToTurn;

            var enemyAnimator = enemyInstance.GetComponentInChildren<EnemyAnimator>();
            var enemyDeath = enemyInstance.GetComponent<EnemyDeath>();
            enemyDeath.Animator = enemyAnimator;

            var enemy = enemyInstance.GetComponent<Enemy>();

            //////////

            CreateWeapon(data, enemyInstance);

            var enemyAttacker = enemyInstance.GetComponent<EnemyAttacker>();

            if (enemyInstance.GetComponentInChildren<RangedEnemyAttacker>() != null)
            {
                var enemyStateMachine = enemyInstance.AddComponent<EnemyRangedStateMachine>();
                enemyStateMachine.Initialize(enemyObserver, enemyMover, enemyAttackTrigger, enemyAttacker, enemyAimer, enemyAnimator, enemyDeath, enemy);
            }
            else if (enemyInstance.GetComponentInChildren<MeleeEnemyAttacker>() != null)
            {
                var enemyStateMachine = enemyInstance.AddComponent<EnemyStateMachine>();
                enemyStateMachine.Initialize(enemyObserver, enemyMover, enemyAttackTrigger, enemyAttacker, enemyAimer, enemyAnimator, enemyDeath, enemy);
            }

            //if(_enemyParentObject != null)
            // _enemyParentObject = Object.Instantiate(_enemyParentObject);

            //enemyInstance.transform.parent = _enemyParentObject.transform;

            return enemyInstance;
        }

        private Weapon CreateWeapon(EnemyData data, Enemy enemyInstance)
        {
            var weaponPoint = enemyInstance.GetComponentInChildren<EnemyModel>().WeaponPoint;

            WeaponData weaponData = null;
            foreach (var weapon in _settings.Weapons)
            {
                if (weapon.Type == data.WeaponType)
                    weaponData = weapon;
            }
            if (weaponData == null)
                Debug.Log("Weapon Data = null");

            Weapon weaponInstance = null;

            if (weaponData.RangeType == WeaponRangeType.Melee)
            {
                weaponInstance = CreateMeleeWeapon(weaponPoint.position, weaponData);
                enemyInstance.AddComponent<MeleeEnemyAttacker>();
                weaponInstance.transform.parent = weaponPoint.transform;
            }
            else if (weaponData.RangeType == WeaponRangeType.Ranged)
            {
                weaponInstance = CreateRangedWeapon(weaponPoint.position, (RangedWeaponData)weaponData);
                weaponInstance.transform.parent = weaponPoint.transform;

                var rangedWeapon = enemyInstance.GetComponentInChildren<RangedWeapon>();
                rangedWeapon.Burrel = enemyInstance.GetComponentInChildren<WeaponModel>().Burrel;

                var attacker = enemyInstance.AddComponent<RangedEnemyAttacker>();
                attacker.Weapon = rangedWeapon;
            }

            return weaponInstance;
        }

        public Weapon CreateMeleeWeapon(Vector3 spawnPosition, WeaponData data)
        {
            var weaponInstance = _assets.Instantiate(Constans.WeaponPath, spawnPosition).AddComponent<Weapon>().GetComponent<Weapon>();
            weaponInstance.Damage = data.Damage;
            weaponInstance.TargetMask = data.TargetMask;
            weaponInstance.RangedType = data.RangeType;
            var weaponModel = weaponInstance.SetModel(data.Model);
            var weaponAttacker = weaponModel.AddComponent<WeaponAttacker>();
            weaponAttacker.Weapon = weaponInstance;

            return weaponInstance;
        }

        public RangedWeapon CreateRangedWeapon(Vector3 spawnPosition, RangedWeaponData data)
        {
            var weaponInstance = _assets.Instantiate(Constans.WeaponPath, spawnPosition).AddComponent<RangedWeapon>().GetComponent<RangedWeapon>();
            weaponInstance.Damage = data.Damage;
            weaponInstance.TargetMask = data.TargetMask;
            weaponInstance.RangedType = data.RangeType;
            weaponInstance.RateOfFire = data.RateOfFire;
            weaponInstance.BulletType = data.BulletType;
            weaponInstance.SetModel(data.Model);

            return weaponInstance;
        }

        public void CreateOverlayPopup() =>
            _assets.Instantiate(Constans.OverlayPopupPath);

        public GameObject CreateStartPopup() =>
            _assets.Instantiate(Constans.StartPopupPath);

        public GameObject CreateStagePopup() =>
            _assets.Instantiate(Constans.StagePopupPath);

        public GameObject CreateVictoryPopup() =>
           _assets.Instantiate(Constans.VictoryPopupPath);
    }
}