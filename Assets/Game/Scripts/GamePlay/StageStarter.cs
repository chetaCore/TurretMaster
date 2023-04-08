using Assets.Game.Scripts.Services;
using Assets.Game.Scripts.Services.GameLoopService;
using UnityEngine;

namespace Assets.Game.Scripts.GamePlay
{
    public class StageStarter : MonoBehaviour
    {
        [SerializeField] private GameObject _particle;
        [SerializeField] private GameObject _DoorToNext;

        private IGameLoopService _gameLoopService;

        public int Id;
        public bool IsActive;

        private void Awake()
        {
            _gameLoopService = AllServices.Container.Single<IGameLoopService>();
        }

        private void Update()
        {
            if (!IsActive) return;

            _particle.SetActive(true);
            GetComponent<BoxCollider>().enabled = true;
            IsActive = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            _DoorToNext.SetActive(false);
            _gameLoopService.ChangeGameLoopState(GameLoopState.StageStarted);
            gameObject.SetActive(false);
        }
    }
}