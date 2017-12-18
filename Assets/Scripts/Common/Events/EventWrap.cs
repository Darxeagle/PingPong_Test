using System;

namespace Assets.Common.Scripts.Events
{
    public class EventWrap : IEventWrap
    {
        private event Action _event;

        public void AddListener(Action handler)
        {
            _event += handler;
        }
        public void RemoveListener(Action handler)
        {
            _event -= handler;
        }

        public void Dispatch()
        {
            if (_event != null) _event();
        }
    }


    public class EventWrap<T> : IEventWrap<T>
    {
        private event Action<T> _event;

        public void AddListener(Action<T> handler)
        {
            _event += handler;
        }
        public void RemoveListener(Action<T> handler)
        {
            _event -= handler;
        }
        public void Dispatch(T arg)
        {
            if (_event != null) _event(arg);
        }
    }

    public class EventWrap<T0,T1> : IEventWrap<T0,T1>
    {
        private event Action<T0, T1> _event;

        public void AddListener(Action<T0, T1> handler)
        {
            _event += handler;
        }
        public void RemoveListener(Action<T0, T1> handler)
        {
            _event -= handler;
        }
        public void Dispatch(T0 arg0, T1 arg1)
        {
            if (_event != null) _event(arg0, arg1);
        }
    }
}
