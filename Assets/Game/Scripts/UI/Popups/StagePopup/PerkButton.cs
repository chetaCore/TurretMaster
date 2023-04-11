using Assets.Game.Scripts.Services;
using Assets.Game.Scripts.Services.GameLoopService;
using Assets.Game.Scripts.Services.GameObjectKeeperService;
using Assets.Game.Scripts.Services.PerkService;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.Scripts.UI.StagePopup.StagePopup
{
    public class PerkButton : MonoBehaviour
    {
        private IPerksService _perksService;
        private IGameLoopService _gameLoopService;

        [SerializeField] private PerkType _type;

        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();

            _perksService = AllServices.Container.Single<IPerksService>();
            _gameLoopService = AllServices.Container.Single<IGameLoopService>();

            _button.onClick.AddListener(UsePerk);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(UsePerk);
        }

        private void UsePerk()
        {
            _perksService.PerkSelector(_type, AllServices.Container.Single<IGameObjectKeeperService>().Player);
            _gameLoopService.ChangeGameLoopState(GameLoopState.VaitingNextStage);
        }
    }
}