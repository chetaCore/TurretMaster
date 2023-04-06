using Assets.Game.Scripts.Infrastructure.GameFactory;
using Assets.Game.Scripts.Services;
using Assets.Game.Scripts.Services.GameLoopService;
using Assets.Game.Scripts.Services.SavesService;
using Assets.Game.Scripts.State;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Game.Scripts.Infrastructure.LevelTest
{
    public class LevelTransferService : ILevelTransferService
    {
        private IGameStateMachine _stateMachine;
        private ILevelsService _levels;
        private IGameLoopService _gameLoopService;

        public LevelTransferService()
        {
            _stateMachine = AllServices.Container.Single<IGameStateMachine>();
            _levels = AllServices.Container.Single<ILevelsService>();
            _gameLoopService = AllServices.Container.Single<IGameLoopService>();

            _gameLoopService.GameLoopStateChangedEvent += LoadSavesLevel;
        }

        private GameObject _currentLevelPref;

        public void LoadSavesScene()
        {
            string activeSceneName = _levels.GetSavedSceneName();

            if (activeSceneName == null)
            {
                Debug.Log("Active scene not found");
                return;
            }

            //if (SceneManager.GetActiveScene().name != activeSceneName)
            //{
                _stateMachine.Enter<LoadLevelState, string>(activeSceneName);
                Debug.Log("<color=cyan>Scene " + activeSceneName + " loaded</color>");
                return;
         //   }

           // Debug.Log("Scene not changed");
        }

        public void LoadSavesLevel(GameLoopState gameLoopState)
        {
            if (gameLoopState != GameLoopState.GameStarted) return;

            var activeLevel = _levels.GetSavedLevel();

            if (activeLevel == null)
            {
                Debug.Log("Active level not found");
                return;
            }
            Object.Destroy(_currentLevelPref);
            _currentLevelPref = Object.Instantiate(activeLevel);
            Debug.Log("<color=green>level " + activeLevel.name + " loaded</color>");
        }
    }
}