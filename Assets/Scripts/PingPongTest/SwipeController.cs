using Assets.Common.Scripts.Events;
using UnityEngine;

namespace Assets.Scripts.PingPongTest
{
    public class SwipeController : MonoBehaviour
    {
        public EventWrap<float> SwipeEvent { get; private set; }

        private Vector3 _mouseLastPosition = Vector3.zero;

        public SwipeController()
        {
            SwipeEvent = new EventWrap<float>();
        }

        void Update()
        {
            //touch
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                SwipeEvent.Dispatch(Input.GetTouch(0).deltaPosition.x);
            }

            //mouse
            if (Input.GetMouseButtonDown(0))
            {
                _mouseLastPosition = Input.mousePosition;
            }
            if (Input.GetMouseButton(0))
            {
                SwipeEvent.Dispatch((Input.mousePosition - _mouseLastPosition).x);
                _mouseLastPosition = Input.mousePosition;
            }
        }
    }
}
