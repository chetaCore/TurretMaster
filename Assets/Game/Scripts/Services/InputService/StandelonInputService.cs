﻿using UnityEngine;

namespace Assets.Game.Scripts.State
{
    public class StandelonInputService : InputService
    {

        public override Vector2 Axis 
        {
            get
            {
                Vector2 axis = SimpleInputAxis();

                if (axis == Vector2.zero)
                    axis = UnityAxis();

                return axis;
            }
        }

        private static Vector2 UnityAxis()
        {
            return new Vector2(Input.GetAxis(Horizontal), Input.GetAxis(Vertical));
        }
    }

}