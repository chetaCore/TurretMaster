using Assets.Game.Scripts.Services.SavesService;
using System;
using UnityEngine;

namespace Assets.Game.Scripts.Services.GameLoopService
{
    public class GameLoopService : IGameLoopService
    {
        private GameLoopState _activeState;

        public GameLoopService()
        {
            ChangeGameLoopState(GameLoopState.VaitingStartGame);
        }

        private GameLoopState ActiveState { get => _activeState; }

        public event Action<GameLoopState> GameLoopStateChangedEvent;

        public void ChangeGameLoopState(GameLoopState gameLoopState)
        {
            GameLoopStateChangedEvent?.Invoke(gameLoopState);
            _activeState = gameLoopState;
            Debug.Log("State change to " + _activeState);
            if(gameLoopState == GameLoopState.Victory)
            {
                AllServices.Container.Single<ILevelsService>().SaveLevel();
            }
        }
    }
}