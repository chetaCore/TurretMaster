using System;

namespace Assets.Game.Scripts.Services.GameLoopService
{
    public interface IGameLoopService : IService
    {
        event Action<GameLoopState> GameLoopStateChangedEvent;

        void ChangeGameLoopState(GameLoopState gameLoopState);
    }
}