using Assets.Common.Scripts.UnityDI;
using UnityEngine;

namespace Assets.Scripts.PingPongTest
{
    public class SingleplayerController : IDependent
    {
        [Dependency] public FieldMediator FieldMediator { private get; set; }
        [Dependency] public DIContainer DiContainer { private get; set; }
        [Dependency] public MenuMediator MenuMediator { private get; set; }
        [Dependency] public SwipeController SwipeController { private get; set; }

        private PhysicsController _physicsController;

        public SingleplayerController()
        {

        }

        public void OnInjected()
        {
            SwipeController.SwipeEvent.AddListener(OnSwipe);

            var gameObejct = new GameObject();
            _physicsController = gameObejct.AddComponent<PhysicsController>();
            DiContainer.BuildUp(_physicsController);

            _physicsController.BallOutBot.AddListener(OnBallOut);
            _physicsController.BallOutTop.AddListener(OnBallOut);

            _physicsController.LaunchBall();

            MenuMediator.gameObject.SetActive(false);
        }

        private void OnSwipe(float distance)
        {
            SetRacketPosition(FieldMediator.Racket1, GetRacketPosition(FieldMediator.Racket1) + distance);
            SetRacketPosition(FieldMediator.Racket2, GetRacketPosition(FieldMediator.Racket2) + distance);
        }

        private float GetRacketPosition(GameObject racket)
        {
            RectTransform racketRectTransform = racket.transform as RectTransform;
            return racketRectTransform.anchoredPosition.x;
        }

        private void SetRacketPosition(GameObject racket, float position)
        {
            RectTransform racketRectTransform = racket.transform as RectTransform;
            racketRectTransform.anchoredPosition = new Vector2(
                Mathf.Clamp(position,
                racketRectTransform.rect.width / 2f,
                FieldMediator.FieldRect.rect.width - racketRectTransform.rect.width / 2f),
                racketRectTransform.anchoredPosition.y);
        }

        private void OnBallOut()
        {
            FieldMediator.ResetState();
            _physicsController.LaunchBall();
        }
    }
}
