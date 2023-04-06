using Assets.Game.Scripts.Infrastructure.GameFactory;
using UnityEngine;

public class AssetProvider : IAssetProvider
{
    public GameObject Instantiate(string path)
    {
        var prefab = Resources.Load<GameObject>(path);
        return Object.Instantiate(prefab);
    }

    public GameObject Instantiate(string path, Vector3 position)
    {
        var prefab = Resources.Load<GameObject>(path);
        return Object.Instantiate(prefab, position, Quaternion.identity);
    }

    public ScriptableObject GetScriptObject(string path)
    {
        return Resources.Load<ScriptableObject>(path);
    }

    public ScriptableObject[] GetAllScriptObject(string path)
    {
        return Resources.LoadAll<ScriptableObject>(path);
    }
}