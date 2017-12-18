namespace Assets.Common.Scripts.Events
{
    interface IEventsContainer
    {
        void AddEvent(string id, IEventWrap eventWrap);
        IEventWrap GetEvent(string id);
    }
}
