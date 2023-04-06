using Assets.Game.Scripts.Infrastructure.GameFactory;
using UnityEngine;

namespace Assets.Game.Scripts.Services.SavesService
{
    public class LevelsService : ILevelsService
    {
        private ISavesService _savesService;
        private IAssetProvider _assets;
        private LevelLoadData _levelLoadData;

        private int _currentScene;
        private int _currentLevel;

       // private int _globalLevel;

        public LevelsService()
        {
            _assets = AllServices.Container.Single<IAssetProvider>();
            _savesService = AllServices.Container.Single<ISavesService>();
            _levelLoadData = (LevelLoadData)_assets.GetScriptObject(Constans.LevelLoadDataPath);

            _currentScene = _savesService.GetInt(Scene);
            _currentLevel = _savesService.GetInt(Level);
        }

        private const string Scene = "Scene";
        private const string Level = "Level";

        public int CurrentScene { get => _currentScene;}
        public int CurrentLevel { get => _currentLevel;}

        public void SaveLevel()
        {
            if (CurrentLevel + 1 >= _levelLoadData.ScenesAndLevels[CurrentScene].Levels.Count)
            {
                if (CurrentScene + 1 >= _levelLoadData.ScenesAndLevels.Count)
                {
                    ChangeSavedScene(0);
                    ChangeSevedLevel(0);
                }
                else
                {
                    ChangeSavedScene(CurrentScene + 1);
                    ChangeSevedLevel(0);
                }
            }
            else
            {
                ChangeSevedLevel(CurrentLevel + 1);
            }
        }

        private void ChangeSevedLevel(int value)
        {
            _savesService.SaveInt(Level, value);
            _currentLevel = value;
        }

        private void ChangeSavedScene(int value)
        {
            _savesService.SaveInt(Scene, value);
            _currentScene = value;
        }

        public string GetSavedSceneName() =>
           _levelLoadData.ScenesAndLevels[CurrentScene].Scene.name;

        public GameObject GetSavedLevel() =>
               _levelLoadData.ScenesAndLevels[CurrentScene].
                Levels[CurrentLevel];
    }
}