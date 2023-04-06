using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameFactorySettings", menuName = "GameFactory/settings")]
public class GameFactorySettings : ScriptableObject
{
    [SerializeField] private PlayerData _player;
    public PlayerData Player
    { get { return _player; } }

    [SerializeField] private List<TurretData> _turrets;
    public List<TurretData> Turrets
    { get { return _turrets; } }

    [SerializeField] private List<ProjectileData> _projectiles;
    public List<ProjectileData> Projectiles
    { get { return _projectiles; } }

    [SerializeField] private List<EnemyData> _enemys;
    public List<EnemyData> Enemys
    { get { return _enemys; } }

    [SerializeField] private List<WeaponData> _weapons;
    public List<WeaponData> Weapons
    { get { return _weapons; } }
}
