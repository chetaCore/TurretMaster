using UnityEngine;

namespace Assets.Game.Scripts.Services.SavesService
{
    public interface ILevelsService : IService
    {
        int CurrentLevel { get; }
        int CurrentScene { get; }

        void SaveLevel();
        string GetSavedSceneName();
        GameObject GetSavedLevel();
    }
}