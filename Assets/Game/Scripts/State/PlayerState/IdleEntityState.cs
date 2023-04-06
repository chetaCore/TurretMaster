using System;
using UnityEngine;

namespace Assets.Game.Scripts.State
{
    public class IdleEntityState : MonoBehaviour, IState
    {
        private EntityStateMachine _entityStateMachine;

        public IdleEntityState(EntityStateMachine entityStateMachine)
        {
            _entityStateMachine = entityStateMachine;
        }

        public void Enter()
        {
            throw new NotImplementedException();
        }

        public void Exit()
        {
            throw new NotImplementedException();
        }
    }
}