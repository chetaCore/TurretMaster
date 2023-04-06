using UnityEngine;

[CreateAssetMenu(fileName = "Bullet", menuName = "Entity/Projectile/Bullet")]
public class BulletData : ProjectileData
{
    [SerializeField] private float _speed;

    public float Speed
    { get { return _speed; } }

    [SerializeField] private float _lifeTime;

    public float LifeTime
    { get { return _lifeTime; } }

    [SerializeField] private GameObject _model;

    public GameObject Model
    { get { return _model; } }
}