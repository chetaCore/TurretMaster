using Assets.Game.Scripts.Infrastructure.LevelTest;
using Assets.Game.Scripts.Services;
using Assets.Game.Scripts.Services.GameLoopService;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.Scripts.UI.Popups
{
    public class VictoryPopup : MonoBehaviour
    {
        [SerializeField] private GameObject _popupGroup;
        [SerializeField] private Button _nextButton;
        private IGameLoopService _gameLoopService;

        private void Awake()
        {
            _gameLoopService = AllServices.Container.Single<IGameLoopService>();
            _nextButton.onClick.AddListener(StartNextLevel);
            _gameLoopService.GameLoopStateChangedEvent += ActivatePopup;
        }

        private void OnDestroy()
        {
            _nextButton.onClick.RemoveListener(StartNextLevel);
            _gameLoopService.GameLoopStateChangedEvent -= ActivatePopup;
        }

        private void ActivatePopup(GameLoopState gameLoopState)
        {
            if (gameLoopState != GameLoopState.Victory) return;

            _popupGroup.SetActive(true);
        }

        private void StartNextLevel()
        {
            _gameLoopService.ChangeGameLoopState(GameLoopState.VaitingStartGame);
            _popupGroup.SetActive(false);
            AllServices.Container.Single<ILevelTransferService>().LoadSavesScene();
        }
    }
}