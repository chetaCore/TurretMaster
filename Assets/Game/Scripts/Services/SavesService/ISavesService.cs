namespace Assets.Game.Scripts.Services.SavesService
{
    public interface ISavesService : IService
    {
        float GetFloat(string key);
        int GetInt(string key);
        void SaveFloat(string key, float value);
        void SaveInt(string key, int value);
    }
}