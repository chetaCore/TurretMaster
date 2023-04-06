using Assets.Game.Scripts.Entity.Turret;
using Assets.Game.Scripts.Services;
using Assets.Game.Scripts.Services.GameLoopService;
using Assets.Game.Scripts.Services.GameObjectKeeperService;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.Scripts.UI.Popups
{
    public class TurretInstallerButton : MonoBehaviour
    {
        [SerializeField] private Button _installButton;
        [SerializeField] private Image _installButtonImage;
        private TurrentInstaller _turrentInstaller;
        private IGameLoopService _gameLoopService;
        private IGameObjectKeeperService _gameObjectKeeperService;
        private TurretData _turretData;

        private void Awake()
        {
            _gameLoopService = AllServices.Container.Single<IGameLoopService>();
            _gameObjectKeeperService = AllServices.Container.Single<IGameObjectKeeperService>();

            _gameLoopService.GameLoopStateChangedEvent += Initialize;
            _installButton.onClick.AddListener(() => InstallButtonClick());
            _turrentInstaller = _gameObjectKeeperService.Player.GetComponent<TurrentInstaller>();
        }

        private void InstallButtonClick()
        {
            if (!_turrentInstaller.CanInstall) return;
            _turrentInstaller.Install();
            _installButtonImage.color = Constans.OpenColor;
            _installButtonImage.fillAmount = 0;

            _turrentInstaller.InstallSeq.Append(_installButtonImage.
                DOFillAmount(1, _gameObjectKeeperService.SelectedTurretData.RechargeTime).
                SetEase(Ease.Linear).
                OnComplete(() => _installButtonImage.color = Constans.ReadyColor));
            _turrentInstaller.InstallSeq.Play();
        }

        private void OnDestroy()
        {
            _gameLoopService.GameLoopStateChangedEvent -= Initialize;
            _installButton.onClick.RemoveListener(() => InstallButtonClick());
        }

        private void Initialize(GameLoopState gameLoopState)
        {
            if (gameLoopState != GameLoopState.GameStarted) return;

            _turretData = _gameObjectKeeperService.SelectedTurretData;
            _installButtonImage.sprite = _turretData.Sprite;
            _installButtonImage.color = Constans.ReadyColor;
        }
    }
}