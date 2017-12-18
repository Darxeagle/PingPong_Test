using Assets.Common.Scripts.Events;

namespace Assets.Common.Scripts.Data
{
    interface IChangeNotifyingField<T>
    {
        IEventWrap Changed { get; }
        T Value { get; set; }
    }

    public class ChangeNotifyingField<T> : IChangeNotifyingField<T>
    {
        private IEventWrap _eventWrap = new EventWrap();
        private T _value = default(T);

        public IEventWrap Changed { get { return _eventWrap; } }

        public T Value {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                _eventWrap.Dispatch();
            }
        }
    }
}
