using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "Entity/Player")]
public class PlayerData : ScriptableObject
{
    [SerializeField] private float _speed;

    public float Speed
    { get { return _speed; } }

    [SerializeField] private float _hp;

    public float HP
    { get { return _hp; } }

    [SerializeField] private float _installTime;

    public float InstallTime
    { get { return _installTime; } }

    [SerializeField] private GameObject _model;

    public GameObject Model
    { get { return _model; } }
}