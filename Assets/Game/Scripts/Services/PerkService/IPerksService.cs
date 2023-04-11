using UnityEngine;

namespace Assets.Game.Scripts.Services.PerkService
{
    public interface IPerksService: IService
    {
        void PerkSelector(PerkType perkType, GameObject perkObject);
    }
}