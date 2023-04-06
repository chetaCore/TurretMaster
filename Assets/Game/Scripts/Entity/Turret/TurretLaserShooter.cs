using Assets.Game.Scripts.Entity.Bullet;
using Assets.Game.Scripts.Entity.Character;
using DG.Tweening;

namespace Assets.Game.Scripts.Entity.Turret
{
    public class TurretLaserShooter : TurretShooter
    {
        private Laser _laser;
        private Sequence _shootSeq;

        private void Start()
        {
            _laser = _factory.CreateLaser(_burrel.transform.position, _projectileType);
            _laser.transform.SetPositionAndRotation(_burrel.transform.position, _burrel.transform.rotation.normalized);
            _laser.gameObject.transform.parent = transform;
            _laser.enabled = false;
        }

        protected override void UseProjectile()
        {
            _laser.LineRenderer.enabled = true;
            _laser.enabled = true;

            _shootSeq = DOTween.Sequence();
            _shootSeq.AppendInterval(_rateOfFire / 2f).OnComplete(() =>
            {
                _laser.LineRenderer.enabled = false;
                _laser.enabled = false;
            });
        }
    }
}