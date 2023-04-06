using UnityEngine;

namespace Assets.Game.Scripts.State
{
    public class MobileInputService : InputService
    {
        public override Vector2 Axis => SimpleInputAxis();
    }

}