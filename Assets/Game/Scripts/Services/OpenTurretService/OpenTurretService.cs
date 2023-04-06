using Assets.Game.Scripts.ScriptableObject;
using Assets.Game.Scripts.Services.SavesService;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Game.Scripts.Services.OpenTurretService
{
    public class OpenTurretService : IOpenTurretService
    {
        private ISavesService _savesService;
        private IAssetProvider _assetProvider;
        private List<TurretData> _turretDatas = new();
        private TurretData _currentTurretData;

        public OpenTurretService()
        {
            _savesService = AllServices.Container.Single<ISavesService>();
            _assetProvider = AllServices.Container.Single<IAssetProvider>();

            foreach (var turretData in _assetProvider.GetAllScriptObject(Constans.TurretsDataPath))
            {
                _turretDatas.Add((TurretData)turretData);
            }

            if (_turretDatas.Count == 0)
            {
                Debug.Log("turret data not found");
                return;
            }

            _turretDatas.Sort(new IdComparer());

            _savesService.SaveFloat(Constans.TurretSaveOpenProgressName + 0, Constans.MaxValueOpeningProgress);

            foreach (var turretData in _turretDatas)
            {

                if (turretData.Id == 0)
                    turretData.OpeningProgress = Constans.MaxValueOpeningProgress;


                if (turretData.OpeningProgress < Constans.MaxValueOpeningProgress)
                {
                    _currentTurretData = turretData;
                    break;
                }
            }
        }

        public TurretData CurrentTurretData { get => _currentTurretData; }

        public bool AddProgress()
        {
            var currentProgress = _savesService.GetFloat(Constans.TurretSaveOpenProgressName + CurrentTurretData.Id);

            if (currentProgress >= Constans.MaxValueOpeningProgress) return true;

            float newProgressValue = currentProgress + Constans.MaxValueOpeningProgress * Constans.TurretOpeningFactor;

            _savesService.SaveFloat(Constans.TurretSaveOpenProgressName + CurrentTurretData.Id, newProgressValue);
            _currentTurretData.OpeningProgress = newProgressValue;

            if (newProgressValue >= Constans.MaxValueOpeningProgress)
                return true;
            else
                return false;
        }
    }
}