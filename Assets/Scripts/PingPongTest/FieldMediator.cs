using UnityEngine;

namespace Assets.Scripts.PingPongTest
{
    public class FieldMediator : MonoBehaviour
    {
        [SerializeField] public RectTransform FieldRect;
        [SerializeField] public GameObject Racket1;
        [SerializeField] public GameObject Racket2;
        [SerializeField] public BallMediator Ball;

        [SerializeField] private Transform BallSpawn;

        public void ResetState()
        {
            Ball.transform.position = BallSpawn.position;
            ResetRacketPosition(Racket1);
            ResetRacketPosition(Racket2);
        }

        private void ResetRacketPosition(GameObject racket)
        {
            RectTransform racketRectTransform = racket.transform as RectTransform;
            racketRectTransform.anchoredPosition = new Vector2(FieldRect.rect.width / 2f, racketRectTransform.anchoredPosition.y);
        }
    }
}
