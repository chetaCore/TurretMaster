using Assets.Game.Scripts.GamePlay;
using Assets.Game.Scripts.Infrastructure.GameFactory;
using Assets.Game.Scripts.Services.GameLoopService;
using Assets.Game.Scripts.Services.SavesService;
using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Game.Scripts.Services.SpawnService
{
    public class SpawnEnemyService : ISpawnEnemyService
    {
        public event Action<int> AllEnemySpawnedEvent;

        private const string SpawnerTag = "Spawn";
        private readonly IGameFactoryService _factory;
        private readonly IAssetProvider _assets;
        private readonly IGameLoopService _gameLoopService;
        private readonly ILevelsService _levelsService;

        private List<EnemySpawnerData> _spawnersData = new();
        private List<Spawn> _spawners = new List<Spawn>();

        private int _countEnemySpawned;
        private int _currentStage;
        private int _countStage;

        public int CurrentStage { get => _currentStage; }
        public int CountStage { get => _countStage; }

        public SpawnEnemyService()
        {
            _factory = AllServices.Container.Single<IGameFactoryService>();
            _assets = AllServices.Container.Single<IAssetProvider>();
            _gameLoopService = AllServices.Container.Single<IGameLoopService>();
            _levelsService = AllServices.Container.Single<ILevelsService>();

            _gameLoopService.GameLoopStateChangedEvent += StartSpawn;

            _currentStage = 0;
        }

        private void RefreshData()
        {
            _spawnersData.Clear();

            var scriptablesData = _assets.GetAllScriptObject
                (
                    Constans.SpawnScriptableObjectsScenesPath + _levelsService.CurrentScene +
                    Constans.SpawnScriptableObjectsLevelsPath + _levelsService.CurrentLevel +
                    Constans.SpawnScriptableObjectsStagesPath + _currentStage
                );

            foreach (var scriptableData in scriptablesData)
            {
                _spawnersData.Add((EnemySpawnerData)scriptableData);
            }

            _countEnemySpawned = 0;
        }

        public void StartSpawn(GameLoopState gameLoopState)
        {
            if (gameLoopState == GameLoopState.GameStarted)
            {
                _countStage = 0;
                SetCountStage();
                _currentStage = 0;
                Debug.Log(_countStage);
            }

            if (gameLoopState != GameLoopState.GameStarted && gameLoopState != GameLoopState.StageStarted) return;

            RefreshData();
            SearchSpawners();
            Vector3 currentSpawn = Vector3.zero;

            foreach (var spawnerData in _spawnersData)
            {
                foreach (var data in spawnerData.TypeAndQualities)
                {
                    _countEnemySpawned += data.Quality;

                    DOTween.Sequence().
                        AppendInterval(spawnerData.SpawnDelay).
                        AppendCallback(() =>
                        {
                            foreach (var spawn in _spawners)
                            {
                                if (spawn.Id == spawnerData.Id)
                                    currentSpawn = spawn.transform.position;
                            }

                            if (currentSpawn == Vector3.zero)
                            {
                                Debug.Log("spawn not found or spawn in zero position");
                                return;
                            }
                            _factory.CreateEnemy(currentSpawn, data.Type);
                        }).
                        SetLoops(data.Quality);
                }
            }
            Debug.Log("<color=green>" + _countEnemySpawned + " противников будет создано</color>");
            if (_countEnemySpawned == 0)
            {
                _gameLoopService.ChangeGameLoopState(GameLoopState.Victory);
                return;
            }

            _currentStage++;
            AllEnemySpawnedEvent?.Invoke(_countEnemySpawned);
        }

        private void SearchSpawners()
        {
            var spawnersObj = GameObject.FindGameObjectsWithTag(SpawnerTag);

            _spawners.Clear();

            foreach (var spawnObj in spawnersObj)
            {
                var spawn = spawnObj.GetComponent<Spawn>();
                if (spawn.Stage == CurrentStage)
                {
                    _spawners.Add(spawn);
                }
            }
            if (_spawners.Count <= 0)
                Debug.Log("<coloe=red>spawners not found</color>");
        }

        private void SetCountStage()
        {
            while (_assets.GetAllScriptObject
                    (
                        Constans.SpawnScriptableObjectsScenesPath + _levelsService.CurrentScene +
                        Constans.SpawnScriptableObjectsLevelsPath + _levelsService.CurrentLevel +
                        Constans.SpawnScriptableObjectsStagesPath + _countStage
                    ).Length != 0
                )
            {
                _countStage++;
            }
        }
    }
}