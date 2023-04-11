using Assets.Game.Scripts.ScriptableObject;
using Assets.Game.Scripts.Services;
using Assets.Game.Scripts.Services.GameLoopService;
using Assets.Game.Scripts.Services.GameObjectKeeperService;
using Assets.Game.Scripts.Services.SavesService;
using Assets.Game.Scripts.UI.TurretCard;
using DanielLochner.Assets.SimpleScrollSnap;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.Scripts.UI.Popups
{
    public class StartPopup : MonoBehaviour
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private SimpleScrollSnap _simpleScrollSnap;
        [SerializeField] private DynamicContent _dynamicContent;
        [SerializeField] private GameObject _popupGroup;

        private ISavesService _savesService;
        private IGameLoopService _gameLoopService;
        private IAssetProvider _assets;
        private IGameObjectKeeperService _gameObjectKeeperService;

        private List<TurretData> _turretDatas = new();

        private void Awake()
        {
            _gameLoopService = AllServices.Container.Single<IGameLoopService>();
            _assets = AllServices.Container.Single<IAssetProvider>();
            _gameObjectKeeperService = AllServices.Container.Single<IGameObjectKeeperService>();
            _savesService = AllServices.Container.Single<ISavesService>();

            _simpleScrollSnap.onPanelCentering.AddListener((int where, int to) =>
            {
                PanelOpennesCheck();
            });
            _startButton.onClick.AddListener(StartGame);
            _gameLoopService.GameLoopStateChangedEvent += ActivatePopup;

            var scriptsObj = _assets.GetAllScriptObject(Constans.TurretsDataPath);

            foreach (var scriptObj in scriptsObj)
                _turretDatas.Add((TurretData)scriptObj);

            _turretDatas.Sort(new IdComparer());
        }

        private void Start()
        {
            foreach (var turretData in _turretDatas)
            {
                var card = _assets.Instantiate(Constans.UITurretCardsPath);
                card.GetComponent<UITurretCard>().TurretData = turretData;

                var image = card.GetComponent<Image>();
                card.GetComponentInChildren<TMP_Text>().text = turretData.Name;

                image.sprite = turretData.Sprite;

                if (_savesService.GetFloat(Constans.TurretSaveOpenProgressName + turretData.Id) < Constans.MaxValueOpeningProgress)
                    image.color = Constans.ClosedColor;

                _dynamicContent.AddToBack(card);
                Destroy(card);
            }
        }

        private void OnDestroy()
        {
            _startButton.onClick.RemoveListener(StartGame);
            _gameLoopService.GameLoopStateChangedEvent -= ActivatePopup;
            _simpleScrollSnap.OnPanelSelecting.RemoveListener(_ =>
            {
                PanelOpennesCheck();
            });
        }

        private void ActivatePopup(GameLoopState gameLoopState)
        {
            if (gameLoopState != GameLoopState.VaitingStartGame) return;

            _popupGroup.SetActive(true);
        }

        private void StartGame()
        {
            _gameObjectKeeperService.
                SelectedTurretData =
            _simpleScrollSnap.
            Panels[_simpleScrollSnap.SelectedPanel].
            GetComponent<UITurretCard>().
            TurretData;

            _gameLoopService.ChangeGameLoopState(GameLoopState.GameStarted);
            _popupGroup.SetActive(false);
        }

        private void PanelOpennesCheck()
        {
            if (_savesService.GetFloat(Constans.TurretSaveOpenProgressName + _simpleScrollSnap.
                Panels[_simpleScrollSnap.SelectedPanel].GetComponent<UITurretCard>().TurretData.Id) < Constans.MaxValueOpeningProgress)
            {
                _startButton.gameObject.SetActive(false);
            }
            else
                _startButton.gameObject.SetActive(true);
        }
    }
}