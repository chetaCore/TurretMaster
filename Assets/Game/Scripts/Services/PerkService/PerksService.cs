using Assets.Game.Scripts.Entity.BaseEntityScripts;
using UnityEngine;

namespace Assets.Game.Scripts.Services.PerkService
{
    public class PerksService : IPerksService
    {
        private const float CharacterSpeedIncreaseFactor = 1.1f;

        public void PerkSelector(PerkType perkType, GameObject perkObject)
        {
            switch (perkType)
            {
                case PerkType.Heal:
                    Heal(perkObject);
                    break;

                case PerkType.AddCharacterSpeed:
                    AddSpeed(perkObject);
                    break;

                default:
                    break;
            }
        }

        private void AddSpeed(GameObject perkObject)
        {
            perkObject.GetComponent<BaseEntity>().Speed *= CharacterSpeedIncreaseFactor;
        }

        private void Heal(GameObject perkObject)
        {
            var entity = perkObject.GetComponent<BaseEntity>();

            entity.CurrentHP = entity.MaxHp;
            perkObject.GetComponentInChildren<HpBar>().ReserHpBar();
        }
    }
}