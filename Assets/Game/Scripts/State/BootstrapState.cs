using Assets.Game.Scripts.Infrastructure.GameFactory;
using Assets.Game.Scripts.Infrastructure.LevelTest;
using Assets.Game.Scripts.Services;
using Assets.Game.Scripts.Services.GameLoopService;
using Assets.Game.Scripts.Services.GameObjectKeeperService;
using Assets.Game.Scripts.Services.OpenTurretService;
using Assets.Game.Scripts.Services.PerkService;
using Assets.Game.Scripts.Services.PoolService;
using Assets.Game.Scripts.Services.SavesService;
using Assets.Game.Scripts.Services.SpawnService;
using Assets.Game.Scripts.Services.VirtualCamerasService;
using UnityEngine;

namespace Assets.Game.Scripts.State
{
    public class BootstrapState : IState
    {
        private const string Boot = "Boot";
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _services = services;

            RegisterServices();
        }

        public void Enter()
        {
            _sceneLoader.Load(Boot, onLoaded: EnterLoadLevel);
        }

        public void Exit()
        {
            //throw new NotImplementedException();
        }

        private void EnterLoadLevel()
        {
            AllServices.Container.Single<ILevelTransferService>().LoadSavesScene();
        }

        private void RegisterServices()
        {
            _services.RegisterSingle<IGameLoopService>(new GameLoopService());
            _services.RegisterSingle<ISavesService>(new SavesService());
            _services.RegisterSingle<IAssetProvider>(new AssetProvider());
            _services.RegisterSingle<ILevelsService>(new LevelsService());
            _services.RegisterSingle<IInputService>(InputService());
            _services.RegisterSingle<IGameStateMachine>(_stateMachine);
            _services.RegisterSingle<IGameFactory>(new GameFactory(AllServices.Container.Single<IAssetProvider>()));
            _services.RegisterSingle<IPoolService>(new PoolService());
            _services.RegisterSingle<ILevelTransferService>(new LevelTransferService());
            _services.RegisterSingle<ISpawnEnemyService>(new SpawnEnemyService());
            _services.RegisterSingle<IGameObjectKeeperService>(new GameObjectKeeperService());
            _services.RegisterSingle<IOpenTurretService>(new OpenTurretService());
            _services.RegisterSingle<IVirtualsCamerasService>(new VirtualsCamerasService());
            _services.RegisterSingle<IPerksService>(new PerksService());
        }

        private static IInputService InputService()
        {
            if (Application.isEditor)
                return new StandelonInputService();
            else
                return new MobileInputService();
        }
    }
}