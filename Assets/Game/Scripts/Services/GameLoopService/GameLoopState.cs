namespace Assets.Game.Scripts.Services.GameLoopService
{
    public enum GameLoopState
    {
        VaitingStartGame,
        GameStarted,
        StageEnded,
        VaitingNextStage,
        StageStarted,
        Defeat,
        Victory,
        VaitingRestartGame,
        VaitingNextLevel
    }
} 