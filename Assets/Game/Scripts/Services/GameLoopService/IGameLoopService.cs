using System;

namespace Assets.Game.Scripts.Services.GameLoopService
{
    public interface IGameLoopService : IService
    {
        GameLoopState ActiveState { get; }

        event Action<GameLoopState> GameLoopStateChangedEvent;

        void ChangeGameLoopState(GameLoopState gameLoopState);
    }
}