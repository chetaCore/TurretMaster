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
            switch (gameLoopState)
            {
                case GameLoopState.VaitingStartGame:
                    _popupGroup.SetActive(false);
                    break;

                case GameLoopState.GameStarted:
                    _popupGroup.SetActive(true);
                    break;

                case GameLoopState.StageEnded:
                    _popupGroup.SetActive(false);
                    break;

                case GameLoopState.VaitingNextStage:
                    _popupGroup.SetActive(true);
                    break;

                case GameLoopState.StageStarted:
                    _popupGroup.SetActive(true);
                    break;

                case GameLoopState.Defeat:
                    _popupGroup.SetActive(false);
                    break;

                case GameLoopState.Victory:
                    _popupGroup.SetActive(false);
                    break;

                default:
                    break;
            }
        }
    }
}