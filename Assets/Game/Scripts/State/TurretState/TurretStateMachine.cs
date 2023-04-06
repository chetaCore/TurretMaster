using Assets.Game.Scripts.Entity.Character;
using Assets.Game.Scripts.Entity.Turret;
using Assets.Game.Scripts.Services;
using Assets.Game.Scripts.Services.GameObjectKeeperService;
using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Game.Scripts.State.TurretState
{
    public class TurretStateMachine : MonoBehaviour
    {
        private IGameObjectKeeperService _gameObjectKeeperService;
        private Dictionary<Type, IState> _states;
        private IState _activeState;

        public void Initialize(TurretAimer turretAimer, TurretObserver turretObserver, TurretShooter turretShooter)
        {
            _gameObjectKeeperService = AllServices.Container.Single<IGameObjectKeeperService>();

            _states = new Dictionary<Type, IState>()
            {
                [typeof(TurretObservingState)] = 
                gameObject.AddComponent<TurretObservingState>().Initialize(this, turretObserver),

                [typeof(TurretShootingState)] =
                gameObject.AddComponent<TurretShootingState>().Initialize(this, turretAimer, turretObserver, turretShooter),
            };
        }

        private void Start()
        {
            transform.localScale = Vector3.zero;
            transform.DOScale(1, _gameObjectKeeperService.Player.GetComponent<TurrentInstaller>().InstallTime).
                SetEase(Ease.Linear).
                OnComplete(() => Enter<TurretObservingState>());
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