using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "LevelLoadData", menuName = "LevelData/LevelLoadData")]
public class LevelLoadData : ScriptableObject
{
    [SerializeField] private List<SceneLevelsStruct> _scenesAndLevels;
    public List<SceneLevelsStruct> ScenesAndLevels
    { get { return _scenesAndLevels; } }


    [System.Serializable]
    public struct SceneLevelsStruct 
    {
        public string Scene;
        public List<GameObject> Levels;
    }
}