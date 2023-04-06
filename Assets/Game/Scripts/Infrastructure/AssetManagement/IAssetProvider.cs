using Assets.Game.Scripts.Services;
using UnityEngine;

public interface IAssetProvider : IService
{
    ScriptableObject GetScriptObject(string path);

    ScriptableObject[] GetAllScriptObject(string path);

    GameObject Instantiate(string path);

    GameObject Instantiate(string path, Vector3 position);
}