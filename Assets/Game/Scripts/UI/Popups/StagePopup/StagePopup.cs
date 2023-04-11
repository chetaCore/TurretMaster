using Assets.Game.Scripts.Services;
using Assets.Game.Scripts.Services.GameLoopService;
using UnityEngine;
using UnityEngine.UI;

public class StagePopup : MonoBehaviour
{
    [SerializeField] private GameObject _popupGroup;
    private IGameLoopService _gameLoopService;

    private void Awake()
    {
        _gameLoopService = AllServices.Container.Single<IGameLoopService>();
        _gameLoopService.GameLoopStateChangedEvent += StartNextStage;
        _gameLoopService.GameLoopStateChangedEvent += ActivatePopup;
    }

    private void OnDestroy()
    {
        _gameLoopService.GameLoopStateChangedEvent -= StartNextStage;
        _gameLoopService.GameLoopStateChangedEvent -= ActivatePopup;
    }

    private void ActivatePopup(GameLoopState gameLoopState)
    {
        if (gameLoopState != GameLoopState.StageEnded) return;
        _popupGroup.SetActive(true);
    }

    private void StartNextStage(GameLoopState gameLoopState)
    {
        if(gameLoopState == GameLoopState.VaitingNextStage)
            _popupGroup.SetActive(false);
    }
}