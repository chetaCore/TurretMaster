using Assets.Game.Scripts.Infrastructure.LevelTest;
using Assets.Game.Scripts.Services;
using Assets.Game.Scripts.Services.GameLoopService;
using Assets.Game.Scripts.State;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.Scripts.UI.Popups
{
    public class DefeatPopup : MonoBehaviour
    {
        [SerializeField] private GameObject _popupGroup;
        [SerializeField] private Button _nextButton;
        private IGameLoopService _gameLoopService;

        private void Awake()
        {
            _gameLoopService = AllServices.Container.Single<IGameLoopService>();
            _nextButton.onClick.AddListener(RestartLevel);
            _gameLoopService.GameLoopStateChangedEvent += ActivatePopup;
        }

        private void OnDestroy()
        {
            _nextButton.onClick.RemoveListener(RestartLevel);
            _gameLoopService.GameLoopStateChangedEvent -= ActivatePopup;
        }

        private void ActivatePopup(GameLoopState gameLoopState)
        {
            if (gameLoopState != GameLoopState.Defeat) return;
            _popupGroup.SetActive(true);
        }

        private void RestartLevel()
        {
            _popupGroup.SetActive(false);
            _gameLoopService.ChangeGameLoopState(GameLoopState.VaitingNextLevel);
            AllServices.Container.Single<IGameStateMachine>().Enter<BootstrapState>();

        }
    }
}