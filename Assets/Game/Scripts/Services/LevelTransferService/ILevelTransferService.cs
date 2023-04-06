using Assets.Game.Scripts.Services;
using Assets.Game.Scripts.Services.GameLoopService;

namespace Assets.Game.Scripts.Infrastructure.LevelTest
{
    public interface ILevelTransferService: IService
    {
        void LoadSavesScene();
        void LoadSavesLevel(GameLoopState gameLoopState);
    }
}