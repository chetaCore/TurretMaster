using Assets.Game.Scripts.Infrastructure.GameFactory;
using Assets.Game.Scripts.Services;
using Assets.Game.Scripts.Services.GameObjectKeeperService;
using DG.Tweening;
using System;
using UnityEngine;

namespace Assets.Game.Scripts.Entity.Turret
{
    public class TurrentInstaller : MonoBehaviour
    {
        public event Action StartInstallEvent;

        private IGameFactory _factory;

        private IGameObjectKeeperService _gameObjectKeeperService;

        private float _installTime;
        private bool _canInstall = true;
        private Sequence _installSeq;

        public Sequence InstallSeq { get => _installSeq;}
        public bool CanInstall { get => _canInstall;}
        public float InstallTime { get => _installTime; set => _installTime = value; }

        private void Awake()
        {
            _factory = AllServices.Container.Single<IGameFactory>();
            _gameObjectKeeperService = AllServices.Container.Single<IGameObjectKeeperService>();
        }

        public void Install()
        {
            if (!CanInstall) return;
            StartInstallEvent?.Invoke();

            _canInstall = false;
            _factory.CreateTurret
                (
                    _gameObjectKeeperService.Player.transform.position + transform.forward,
                    _gameObjectKeeperService.SelectedTurretData.Type
                );

            _installSeq = DOTween.Sequence();

            InstallSeq.OnKill(() =>
            {
                _canInstall = true;
                InstallSeq.Kill();
            });
        }
    }
}