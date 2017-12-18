using System;
using System.Collections.Generic;

namespace Assets.Common.Scripts.Events
{
    class EventsContainer : IEventsContainer
    {
        private Dictionary<string, IEventWrap> _events = new Dictionary<string, IEventWrap>();

        public void AddEvent(string id, IEventWrap eventWrap)
        {
            if (_events.ContainsKey(id)) throw new Exception("EventsContainer already contains this id");
            _events.Add(id, eventWrap);
        }

        public IEventWrap GetEvent(string id)
        {
            if (!_events.ContainsKey(id)) throw new Exception("EventsContainer does not contain this id");
            return _events[id];
        }
    }
}
