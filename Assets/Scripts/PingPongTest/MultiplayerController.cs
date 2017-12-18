using System.Collections.Generic;
using Assets.Common.Scripts;
using Assets.Common.Scripts.UnityDI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.PingPongTest
{
    public class MultiplayerController : IDependent
    {
        [Dependency] public FieldMediator FieldMediator { private get; set; }
        [Dependency] public DIContainer DiContainer { private get; set; }
        [Dependency] public MenuMediator MenuMediator { private get; set; }
        [Dependency] public PhotonServerController PhotonServerController { private get; set; }
        [Dependency] public MonoProvider MonoProvider { private get; set; }
        [Dependency] public SwipeController SwipeController { private get; set; }

        private PhysicsController _physicsController;
        private bool isHost;

        public MultiplayerController()
        {

        }

        public void OnInjected()
        {
            PhotonServerController.CreatedGame.AddListener(OnHost);
            PhotonServerController.JoinedGame.AddListener(OnClient);
            PhotonServerController.Disconnected.AddListener(OnDisconnected);
            PhotonServerController.ReceivedEvent.AddListener(OnEventReceived);

            PhotonServerController.FindGame();
        }

        private void OnEventReceived(byte code, Dictionary<byte, object> data)
        {
            if (isHost)
            {
                if (code == 1)
                {
                    SetRacketPosition(FieldMediator.Racket2, (float)data[0]);
                }
            }
            else
            {
                if (code == 1)
                {
                    SetRacketPosition(FieldMediator.Racket1, (float)data[0]);
                    FieldMediator.Ball.Rigidbody2D.position = new Vector2((float)data[1], (float)data[2]);
                    FieldMediator.Ball.Rigidbody2D.velocity = new Vector2((float)data[3], (float)data[4]);
                }
                if (code == 2)
                {
                    FieldMediator.ResetState();
                }
            }
        }

        private void OnDisconnected()
        {
            PhotonServerController.Disconnect();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        private void OnHost()
        {
            isHost = true;

            SwipeController.SwipeEvent.AddListener(OnSwipe);
            MonoProvider.FixedUpdateEvent.AddListener(OnUpdate);

            var gameObejct = new GameObject();
            _physicsController = gameObejct.AddComponent<PhysicsController>();
            DiContainer.BuildUp(_physicsController);

            _physicsController.BallOutBot.AddListener(OnBallOut);
            _physicsController.BallOutTop.AddListener(OnBallOut);

            _physicsController.LaunchBall();

            MenuMediator.gameObject.SetActive(false);
        }

        private void OnClient()
        {
            isHost = false;

            SwipeController.SwipeEvent.AddListener(OnSwipe);
            MonoProvider.FixedUpdateEvent.AddListener(OnUpdate);

            MenuMediator.gameObject.SetActive(false);
        }

        private void OnSwipe(float distance)
        {
            if (isHost) SetRacketPosition(FieldMediator.Racket1, GetRacketPosition(FieldMediator.Racket1) + distance);
            if (!isHost) SetRacketPosition(FieldMediator.Racket2, GetRacketPosition(FieldMediator.Racket2) + distance);
        }

        private void OnUpdate(float obj)
        {
            if (isHost)
            {
                var dict = new Dictionary<byte, object>();
                dict.Add(0, (FieldMediator.Racket1.transform as RectTransform).anchoredPosition.x);
                dict.Add(1, FieldMediator.Ball.Rigidbody2D.position.x);
                dict.Add(2, FieldMediator.Ball.Rigidbody2D.position.y);
                dict.Add(3, FieldMediator.Ball.Rigidbody2D.velocity.x);
                dict.Add(4, FieldMediator.Ball.Rigidbody2D.velocity.y);
                PhotonServerController.SendEvent(1, dict);
            }
            else
            {
                var dict = new Dictionary<byte, object>();
                dict.Add(0, (FieldMediator.Racket2.transform as RectTransform).anchoredPosition.x);
                PhotonServerController.SendEvent(1, dict);
            }
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
            if (isHost)
            {
                FieldMediator.ResetState();
                _physicsController.LaunchBall();
                PhotonServerController.SendEvent(2, new Dictionary<byte, object>());
            }
        }
    }
}
