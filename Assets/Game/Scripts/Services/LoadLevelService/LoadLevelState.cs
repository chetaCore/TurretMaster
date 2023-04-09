using Assets.Game.Scripts.Infrastructure.GameFactory;
using Assets.Game.Scripts.Infrastructure.LevelTest;
using Assets.Game.Scripts.Services;
using Assets.Game.Scripts.Services.GameObjectKeeperService;
using Assets.Game.Scripts.Services.VirtualCamerasService;
using UnityEngine;
using static Assets.Game.Scripts.State.GameStateMachine;

namespace Assets.Game.Scripts.State
{
    public class LoadLevelState : IPayLoadedState<string>
    {
        private const string PlayerSpawnPoint = "PlayerSpawnPoint";
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly PopupController _popupController;
        private readonly IGameFactory _gameFactory;
        private readonly IGameObjectKeeperService _gameObjectKeeperService;


        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, PopupController popupController, IGameFactory gameFactory)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _popupController = popupController;
            _gameFactory = gameFactory;

            _gameObjectKeeperService = AllServices.Container.Single<IGameObjectKeeperService>();
        }

        public void Enter(string sceneName)
        {
            _popupController.Show();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        private void OnLoaded()
        {
            _gameObjectKeeperService.Player = _gameFactory.CreateHero(GameObject.FindWithTag(PlayerSpawnPoint));
            _gameFactory.CreateOverlayPopup();
            _gameFactory.CreateStartPopup();
            _gameFactory.CreateStagePopup();
            _gameFactory.CreateVictoryPopup();
            AllServices.Container.Single<IVirtualsCamerasService>().CreateCameras();
           
            _stateMachine.Enter<GameLoopState>();
        }

        public void Exit()
        {
            _popupController.Hide();
        }
    }
}