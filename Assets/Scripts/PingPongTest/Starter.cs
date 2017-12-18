using Assets.Common.Scripts;
using Assets.Common.Scripts.UnityDI;
using UnityEngine;

namespace Assets.Scripts.PingPongTest
{
    class Starter : MonoBehaviour
    {
        [SerializeField] private FieldMediator FieldMediator;
        [SerializeField] private MenuMediator MenuMediator;

        private DIContainer _diContainer;

        void Start()
        {
            Application.runInBackground = true;

            _diContainer = new DIContainer();
            var swipeController = gameObject.AddComponent<SwipeController>();
            var monoProvider = gameObject.AddComponent<MonoProvider>();

            _diContainer.RegisterInstance<DIContainer>(_diContainer);
            _diContainer.RegisterInstance<FieldMediator>(FieldMediator);
            _diContainer.RegisterInstance<MenuMediator>(MenuMediator);
            _diContainer.RegisterInstance<SwipeController>(swipeController);
            _diContainer.RegisterInstance<MonoProvider>(monoProvider);
            _diContainer.RegisterSingleton<PhotonServerController>();

            _diContainer.BuildUp(MenuMediator);
        }
    }
}
