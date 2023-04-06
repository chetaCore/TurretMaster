using Assets.Game.Scripts.Services;
using Assets.Game.Scripts.Services.OpenTurretService;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.Scripts.UI.Popups.VictoryPopups
{
    public class TurretOpenProgressWindow : MonoBehaviour
    {
        private IOpenTurretService _openTurretService;

        [SerializeField] private Image _image;
        [SerializeField] private Image _progressLine;

        public float FillAmountDuration;

        private void Awake()
        {
            _openTurretService = AllServices.Container.Single<IOpenTurretService>();
        }

        private void OnEnable()
        {
            _image.sprite = _openTurretService.CurrentTurretData.Sprite;

            if (_openTurretService.CurrentTurretData.OpeningProgress >= Constans.MaxValueOpeningProgress)
            {
                _progressLine.fillAmount = 1;
                return;
            }

            _image.color = Constans.ClosedColor;
            var isOpened = _openTurretService.AddProgress();

            _progressLine.DOFillAmount(_openTurretService.CurrentTurretData.OpeningProgress / Constans.MaxValueOpeningProgress, FillAmountDuration)
                .OnComplete(() =>
                {
                    if (isOpened)
                        _image.color = Constans.OpenColor;
                });
        }
    }
}