using Assets.Game.Scripts.Services;
using UnityEngine;

namespace Assets.Game.Scripts.State
{
    public interface IInputService : IService
    {
        Vector2 Axis { get; }

        public bool IsBuildButtonDown();
    }
}