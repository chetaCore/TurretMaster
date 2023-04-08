using Assets.Game.Scripts.Services;
using Assets.Game.Scripts.Services.GameLoopService;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.Scripts.UI.Popups
{
    public class StagePopup : MonoBehaviour
    {
        [SerializeField] private Button _nextButton;
        [SerializeField] private GameObject _popupGroup;
        private IGameLoopService _gameLoopService;

        private void Awake()
        {
            _gameLoopService = AllServices.Container.Single<IGameLoopService>();
            _nextButton.onClick.AddListener(StartNextStage);
            _gameLoopService.GameLoopStateChangedEvent += ActivatePopup;
        }

        private void OnDestroy()
        {
            _nextButton.onClick.RemoveListener(StartNextStage);
            _gameLoopService.GameLoopStateChangedEvent -= ActivatePopup;
        }

        private void ActivatePopup(GameLoopState gameLoopState)
        {
            if (gameLoopState != GameLoopState.StageEnded) return;
            _popupGroup.SetActive(true);

        }

        private void StartNextStage()
        {
             _gameLoopService.ChangeGameLoopState(GameLoopState.VaitingNextStage);
            _popupGroup.SetActive(false);
        }




    }
}