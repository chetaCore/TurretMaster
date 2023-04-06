using Assets.Game.Scripts.Services.GameLoopService;
using System;

namespace Assets.Game.Scripts.Services.SpawnService
{
    public interface ISpawnEnemyService : IService
    {
        int CurrentStage { get; }
        int CountStage { get; }

        event Action<int> AllEnemySpawnedEvent;

        void StartSpawn(GameLoopState gameLoopState);
    }
}