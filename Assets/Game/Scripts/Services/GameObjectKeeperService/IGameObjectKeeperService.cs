using System;
using UnityEngine;

namespace Assets.Game.Scripts.Services.GameObjectKeeperService
{
    public interface IGameObjectKeeperService : IService
    {
        TurretData SelectedTurretData { get; set; }
        GameObject StartPopup { get; set; }
        GameObject Player { get; set; }


        void DecreaseCountLivingEnemy();
    }
}