using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.Scripts.Entity.BaseEntityScripts
{
    public class HpBar : MonoBehaviour
    {
        [SerializeField] private Image _hpBar;
        [SerializeField] private BaseEntity _entity;

        private void Awake()
        {
            _entity.TakeDamageEvent += () => ChangeValue(_entity.CurrentHP, _entity.MaxHp);
        }
        private void OnDestroy()
        {
            _entity.TakeDamageEvent -= () => ChangeValue(_entity.CurrentHP, _entity.MaxHp);

        }

        private void ChangeValue(float current, float max) =>
            _hpBar.fillAmount = current / max;

        private void Update()
        {
            transform.LookAt(Camera.main.transform.position);
        }
    }
}