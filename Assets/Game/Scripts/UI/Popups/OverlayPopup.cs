using Assets.Game.Scripts.Services;
using Assets.Game.Scripts.Services.GameLoopService;
using UnityEngine;

namespace Assets.Game.Scripts.UI.Popups
{
    public class OverlayPopup : MonoBehaviour
    {
        [SerializeField] private GameObject _popupGroup;
        private IGameLoopService _gameLoopService;

        private void Awake()
        {
            _gameLoopService = AllServices.Container.Single<IGameLoopService>();
            _gameLoopService.GameLoopStateChangedEvent += ChangeActivity;
        }

        private void OnDestroy()
        {
            _gameLoopService.GameLoopStateChangedEvent -= ChangeActivity;
        }

        private void ChangeActivity(GameLoopState gameLoopState)
        {
            if (gameLoopState == GameLoopState.GameStarted)
                _popupGroup.SetActive(true);

            if (gameLoopState == GameLoopState.VaitingStartGame)
                _popupGroup.SetActive(false);
        }
    }
}