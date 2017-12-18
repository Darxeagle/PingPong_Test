using Assets.Common.Scripts.Events;
using UnityEngine;

namespace Assets.Common.Scripts
{
    public class MonoProvider : MonoBehaviour
    {
        public EventWrap<float> UpdateEvent = new EventWrap<float>();
        public EventWrap<float> FixedUpdateEvent = new EventWrap<float>();
        public EventWrap ApplicationQuit = new EventWrap();

        void Update()
        {
            UpdateEvent.Dispatch(Time.deltaTime);
        }

        void FixedUpdate()
        {
            FixedUpdateEvent.Dispatch(Time.fixedDeltaTime);
        }

        void OnApplicationQuit()
        {
            ApplicationQuit.Dispatch();
        }
    }
}
