using System.Globalization;
using UnityEngine;

namespace Assets.Game.Scripts.State
{
    public class GameBootstrapper : MonoBehaviour, ICorutineRunner
    {
        private Game _game;

        public PopupController PopupControllerPrefab;

        private void Awake()
        {
            _game = new Game(this, Instantiate(PopupControllerPrefab));
            _game.StateMachine.Enter<BootstrapState>();

            DontDestroyOnLoad(this);
        }

    }
}