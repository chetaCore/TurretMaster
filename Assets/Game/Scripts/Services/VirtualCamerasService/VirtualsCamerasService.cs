using Assets.Game.Scripts.Infrastructure.GameFactory;
using Assets.Game.Scripts.Services.GameLoopService;
using Cinemachine;
using System.Collections;
using UnityEngine;

namespace Assets.Game.Scripts.Services.VirtualCamerasService
{
    public class VirtualsCamerasService : IVirtualsCamerasService
    {
        private IAssetProvider _assetProvider;
        private IGameLoopService _gameLoopService;
        private IGameFactoryService _gameFactory;


        GameObject _virtualsCamerasPrefab;

        public VirtualsCamerasService()
        {
            _assetProvider = AllServices.Container.Single<IAssetProvider>();
            _gameLoopService = AllServices.Container.Single<IGameLoopService>();
            _gameFactory = AllServices.Container.Single<IGameFactoryService>();

            _gameLoopService.GameLoopStateChangedEvent += (_) =>
            {
                if (_ == GameLoopState.GameStarted)
                {
                    _virtualsCamerasPrefab.SetActive(false);
                    Camera.main.GetComponent<CinemachineBrain>().enabled = false;
                }

                    _virtualsCamerasPrefab = _assetProvider.Instantiate(Constans.VirtualsCamerasPath);

            };
        }

        public void CreateCameras()
        {
            _virtualsCamerasPrefab = _gameFactory.CreateVirtualsCameras();
        }
    }
}