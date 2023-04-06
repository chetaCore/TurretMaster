using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Game.Scripts.State
{
    public class EntityStateMachine : MonoBehaviour
    {
        [SerializeField] private CharacterMover _characterMover;

        private readonly Dictionary<Type, IState> _states;
        private IState _activeState;

        public EntityStateMachine()
        {
            _states = new Dictionary<Type, IState>()
            {
                [typeof(IdleEntityState)] = new IdleEntityState(this),
                [typeof(MovementState)] = new MovementState(_characterMover),
                [typeof(TurretInstallerState)] = new TurretInstallerState(),
            };
        }

        public void Enter<TState>() where TState : IState
        {
            _activeState?.Exit();
            IState state = _states[typeof(TState)];
            _activeState = state;
            state.Enter();
        }
    }
}