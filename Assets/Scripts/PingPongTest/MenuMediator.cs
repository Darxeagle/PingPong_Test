using Assets.Common.Scripts.Factories;
using Assets.Common.Scripts.UnityDI;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.PingPongTest
{
    public class MenuMediator : MonoBehaviour
    {
        [Dependency] public DIContainer DIContainer { set; private get; }

        [SerializeField] private GameObject ButtonsContainer;
        [SerializeField] private GameObject ConnectingContainer;
        [SerializeField] private Button Singleplayer;
        [SerializeField] private Button Multiplayer;

        void Start()
        {
            Singleplayer.onClick.AddListener(OnSingleplayer);
            Multiplayer.onClick.AddListener(OnMultiplayer);
        }

        private void OnMultiplayer()
        {
            ButtonsContainer.SetActive(false);
            ConnectingContainer.SetActive(true);

            var controller = new MultiplayerController();
            DIContainer.BuildUp(controller);
        }

        private void OnSingleplayer()
        {
            var controller = new SingleplayerController();
            DIContainer.BuildUp(controller);
        }
    }
}
