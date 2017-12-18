using System;

namespace Assets.Common.Scripts.Events
{
    public interface IEventWrap
    {
        void AddListener(Action handler);
        void RemoveListener(Action handler);
        void Dispatch();
    }

    public interface IEventWrap<T>
    {
        void AddListener(Action<T> handler);
        void RemoveListener(Action<T> handler);
        void Dispatch(T arg);
    }

    public interface IEventWrap<T0, T1>
    {
        void AddListener(Action<T0, T1> handler);
        void RemoveListener(Action<T0, T1> handler);
        void Dispatch(T0 arg0, T1 arg1);
    }
}
