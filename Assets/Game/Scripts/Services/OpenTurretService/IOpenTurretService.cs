namespace Assets.Game.Scripts.Services.OpenTurretService
{
    public interface IOpenTurretService : IService
    {
        TurretData CurrentTurretData { get; }

        bool AddProgress();
    }
}