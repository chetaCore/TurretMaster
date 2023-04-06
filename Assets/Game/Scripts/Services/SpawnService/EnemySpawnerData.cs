using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spawn", menuName = "Spawners/EnemySpawn")]
public class EnemySpawnerData : ScriptableObject
{
    [SerializeField] private int _id;

    public int Id
    { get { return _id; } }

    [SerializeField] private WhenToUseStr _whenToUse;

    public WhenToUseStr WhenToUse
    { get { return _whenToUse; } }

    [SerializeField] private List<TypeAndQuality> _typeAndQualities;

    public List<TypeAndQuality> TypeAndQualities
    { get { return _typeAndQualities; } }

    [SerializeField] private float _spawnDelay;

    public float SpawnDelay
    { get { return _spawnDelay; } }

    [System.Serializable]
    public struct TypeAndQuality
    {
        public EnemyType Type;
        public int Quality;
    }

    [System.Serializable]
    public struct WhenToUseStr
    {
        public int Scene;
        public int Level;
        public int Stage;
    }
}