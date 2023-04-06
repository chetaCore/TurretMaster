using UnityEditor;
using UnityEngine;

namespace Assets.Game.Scripts.Services.SavesService
{
    public class SavesService : ISavesService
    {
        public int GetInt(string key)
        {
            return PlayerPrefs.GetInt(key);
        }

        public void SaveInt(string key, int value)
        {
            Debug.Log("<color=yellow>" + key + " saved, value: " + value + "</color>");
            PlayerPrefs.SetInt(key, value);
        }

        public float GetFloat(string key)
        {
            return PlayerPrefs.GetFloat(key);
        }

        public void SaveFloat(string key, float value)
        {
            Debug.Log("<color=yellow>" + key + " saved, value: " + value + "</color>");
            PlayerPrefs.SetFloat(key, value);
        }
    }
}