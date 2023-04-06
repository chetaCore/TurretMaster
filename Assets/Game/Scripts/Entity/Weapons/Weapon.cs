using UnityEngine;

namespace Assets.Game.Scripts.Entity.Weapons
{
    public class Weapon : MonoBehaviour
    {
        private WeaponType _type;
        private WeaponRangeType _rangedType;
        private LayerMask _targetMask;
        private float _damage;
        private GameObject _model;

        public GameObject Model { get => _model; }
        public WeaponType WeaponType { get => _type; set => _type = value; }
        public LayerMask TargetMask { get => _targetMask; set => _targetMask = value; }
        public float Damage { get => _damage; set => _damage = value; }
        public WeaponRangeType RangedType { get => _rangedType; set => _rangedType = value; }

        public GameObject SetModel(GameObject model)
        {
            if (model != null)
                Destroy(Model);

            _model = Instantiate(model, transform.position, Quaternion.identity, transform);
            return _model;
        }








    }
}