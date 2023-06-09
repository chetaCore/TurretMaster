﻿using Assets.Game.Scripts.Services;
using Assets.Game.Scripts.Services.GameLoopService;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.Scripts.Entity.BaseEntityScripts
{
    public class HpBar : MonoBehaviour
    {
        private IGameLoopService _gameLoopService;

        [SerializeField] private Image _hpBar;
        [SerializeField] private BaseEntity _entity;

        private void Awake()
        {
            _gameLoopService = AllServices.Container.Single<IGameLoopService>();

            _entity.TakeDamageEvent += () => ChangeValue(_entity.CurrentHP, _entity.MaxHp);

            _gameLoopService.GameLoopStateChangedEvent += GameLoopBehavior;
        }

        private void OnDestroy()
        {
            _entity.TakeDamageEvent -= () => ChangeValue(_entity.CurrentHP, _entity.MaxHp);

            _gameLoopService.GameLoopStateChangedEvent -= GameLoopBehavior;
        }

        private void GameLoopBehavior(GameLoopState gameLoopState)
        {
            switch (gameLoopState)
            {
                case GameLoopState.VaitingStartGame:
                    _hpBar.enabled = false;
                    break;

                case GameLoopState.GameStarted:
                    _hpBar.enabled = true;
                    break;

                default:
                    break;
            }
        }

        public void ReserHpBar() =>
            _hpBar.DOFillAmount(1, 1f);

        private void ChangeValue(float current, float max) =>
            _hpBar.fillAmount = current / max;

        private void Update()
        {
            transform.LookAt(Camera.main.transform.position);
        }
    }
}