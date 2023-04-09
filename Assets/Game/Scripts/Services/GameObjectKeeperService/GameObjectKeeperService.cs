using Assets.Game.Scripts.GamePlay;
using Assets.Game.Scripts.Services.GameLoopService;
using Assets.Game.Scripts.Services.SavesService;
using Assets.Game.Scripts.Services.SpawnService;
using DG.Tweening;
using UnityEngine;

namespace Assets.Game.Scripts.Services.GameObjectKeeperService
{
    public class GameObjectKeeperService : IGameObjectKeeperService
    {
        private TurretData _selectedTurretData;
        private GameObject _startPopup;
        private GameObject _player;
        private ISpawnEnemyService _spawnEnemyServise;
        private IGameLoopService _gameLoopService;
        private ILevelsService _levelService;

        private int _countLivingEnemy;

        public GameObjectKeeperService()
        {
            _spawnEnemyServise = AllServices.Container.Single<ISpawnEnemyService>();
            _gameLoopService = AllServices.Container.Single<IGameLoopService>();
            _levelService = AllServices.Container.Single<ILevelsService>();
            _spawnEnemyServise.AllEnemySpawnedEvent += (int value) => CountLivingEnemy = value;
            _gameLoopService.GameLoopStateChangedEvent += (_) =>
            {
                if (_ == GameLoopState.GameStarted)
                    foreach (var LevelPlayerPoint in GameObject.FindGameObjectsWithTag("LevelPlayerPoint"))
                    {
                        if (LevelPlayerPoint.GetComponent<LevelPlayerPoint>().Id == _levelService.CurrentLevel)
                        {
                            DOTween.Sequence().AppendInterval(0).
                            OnComplete(() =>
                            {
                                while (_player.gameObject.transform.position != LevelPlayerPoint.transform.position)
                                    _player.gameObject.transform.position = LevelPlayerPoint.transform.position;
                            });
                            break;
                        }
                    }
            };
        }

        public TurretData SelectedTurretData { get => _selectedTurretData; set => _selectedTurretData = value; }
        public GameObject StartPopup { get => _startPopup; set => _startPopup = value; }
        public GameObject Player { get => _player; set => _player = value; }
        public int CountLivingEnemy { get => _countLivingEnemy; set => _countLivingEnemy = value; }

        public void DecreaseCountLivingEnemy()
        {
            CountLivingEnemy--;
            if (CountLivingEnemy <= 0)
                if (_spawnEnemyServise.CurrentStage == _spawnEnemyServise.CountStage)
                {
                    _gameLoopService.ChangeGameLoopState(GameLoopState.Victory);
                    return;
                }
                else
                {
                    foreach (var stage in GameObject.FindGameObjectsWithTag(Constans.StageStarerName))
                    {
                        var currentStage = stage.GetComponent<StageStarter>();

                        if (currentStage.Id == _spawnEnemyServise.CurrentStage)
                        {
                            currentStage.IsActive = true;
                            break;
                        }
                        Debug.Log("Stage not found");
                    }
                    _gameLoopService.ChangeGameLoopState(GameLoopState.StageEnded);
                }
        }
    }
}