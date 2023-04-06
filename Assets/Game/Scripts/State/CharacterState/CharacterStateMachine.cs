using Assets.Game.Scripts.Entity.Character;
using Assets.Game.Scripts.Entity.Turret;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Game.Scripts.State.CharacterState
{
    public class CharacterStateMachine : MonoBehaviour
    {
        private Player _character;
        private Dictionary<Type, IState> _states;
        private IState _activeState;

        public void Initialize(Player character, CharacterMover characterMover, TurrentInstaller turrentInstaller, CharacterAnimator animator)
        {
            _character = character;

            _states = new Dictionary<Type, IState>()
            {
                [typeof(CharacterIdleState)] =
                gameObject.AddComponent<CharacterIdleState>().Initialize(this),

                [typeof(CharacterMovingState)] =
                gameObject.AddComponent<CharacterMovingState>().Initialize(this, characterMover, turrentInstaller),

                [typeof(CharacterInstallState)] =
                gameObject.AddComponent<CharacterInstallState>().Initialize(this, turrentInstaller, animator),

                [typeof(CharacterDeathState)] =
                gameObject.AddComponent<CharacterDeathState>().Initialize(this),
            };

            _character.DeathEvent += () => Enter<CharacterDeathState>();
        }

        private void Start()
        {
            Enter<CharacterDeathState>();
        }

        private void OnDestroy()
        {
            _character.DeathEvent -= () => Enter<CharacterDeathState>();
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