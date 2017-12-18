namespace Assets.Common.Scripts.Data
{
    public interface IValueDict<T0, T1>
    {
        void Add(T0 id, T1 item);
        void Remove(T0 id);
        bool Contains(T0 id);
        T1 Get(T0 id);
    }
}
