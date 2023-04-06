using UnityEngine;

namespace Assets.Game.Scripts.GamePlay
{
    public class Spawn : MonoBehaviour
    {
        [SerializeField] private int _id;
        [SerializeField] private int _stage;

        public int Id { get => _id;}
        public int Stage { get => _stage;}
    }
}