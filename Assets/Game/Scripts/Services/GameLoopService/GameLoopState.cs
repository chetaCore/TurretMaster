namespace Assets.Game.Scripts.Services.GameLoopService
{
    public enum GameLoopState
    {
        VaitingStartGame,
        GameStarted,
        StageEnded,
        StageStarted,
        Defeat,
        Victory,
        VaitingRestartGame,
        VaitingNextLevel
    }
}