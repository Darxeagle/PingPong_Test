using Assets.Common.Scripts.Events;
using Assets.Common.Scripts.UnityDI;
using UnityEngine;

namespace Assets.Scripts.PingPongTest
{
    public class PhysicsController : MonoBehaviour, IDependent
    {
        [Dependency] public FieldMediator FieldMediator { private get; set; }

        public EventWrap BallOutTop { get; private set; }
        public EventWrap BallOutBot { get; private set; }

        public void OnInjected()
        {
            FieldMediator.Ball.CollisionEvent.AddListener(OnBallCollision);
            BallOutTop = new EventWrap();
            BallOutBot = new EventWrap();
        }

        public void LaunchBall()
        {
            float velX = Random.Range(-10, 10);
            float velY = 10*(Random.value > 0.5f ? 1 : -1);
            FieldMediator.Ball.Rigidbody2D.velocity = new Vector2(velX, velY);
        }

        private void OnBallCollision(GameObject col)
        {
            if (col == FieldMediator.Racket1 && FieldMediator.Ball.Rigidbody2D.velocity.y < 0)
            {
                FieldMediator.Ball.Rigidbody2D.velocity = new Vector2(FieldMediator.Ball.Rigidbody2D.velocity.x, -FieldMediator.Ball.Rigidbody2D.velocity.y);
            }
            if (col == FieldMediator.Racket2 && FieldMediator.Ball.Rigidbody2D.velocity.y > 0)
            {
                FieldMediator.Ball.Rigidbody2D.velocity = new Vector2(FieldMediator.Ball.Rigidbody2D.velocity.x, -FieldMediator.Ball.Rigidbody2D.velocity.y);
            }
        }

        void FixedUpdate()
        {
            RectTransform ballTransform = FieldMediator.Ball.transform as RectTransform;

            if (ballTransform.anchoredPosition.y > FieldMediator.FieldRect.rect.height)
            {
                BallOutTop.Dispatch();
            }
            if (ballTransform.anchoredPosition.y < 0)
            {
                BallOutBot.Dispatch();
            }

            if (ballTransform.anchoredPosition.x > FieldMediator.FieldRect.rect.width && FieldMediator.Ball.Rigidbody2D.velocity.x > 0)
            {
                FieldMediator.Ball.Rigidbody2D.velocity = new Vector2(-FieldMediator.Ball.Rigidbody2D.velocity.x, FieldMediator.Ball.Rigidbody2D.velocity.y);
            }
            if (ballTransform.anchoredPosition.x < 0 && FieldMediator.Ball.Rigidbody2D.velocity.x < 0)
            {
                FieldMediator.Ball.Rigidbody2D.velocity = new Vector2(-FieldMediator.Ball.Rigidbody2D.velocity.x, FieldMediator.Ball.Rigidbody2D.velocity.y);
            }
        }
    }
}
