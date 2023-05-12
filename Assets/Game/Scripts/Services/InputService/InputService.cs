using UnityEngine;

namespace Assets.Game.Scripts.State
{
    public abstract class InputService : IInputService
    {
        protected const string Vertical = "Vertical";
        protected const string Horizontal = "Horizontal";
        private const string Build = "Build";

        public abstract Vector2 Axis { get; }

        bool IInputService.IsBuildButtonDown() => Input.GetKeyDown(KeyCode.Q);

        protected static Vector2 SimpleInputAxis() => new Vector2(SimpleInput.GetAxis(Horizontal), SimpleInput.GetAxis(Vertical));
    }
}