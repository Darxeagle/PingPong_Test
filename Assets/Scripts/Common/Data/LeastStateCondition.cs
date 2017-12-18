using System.Collections.Generic;
using Assets.Common.Scripts.Events;

namespace Assets.Common.Scripts.Data
{
    class LeastStateCondition : IStateCondition
    {
        public EventWrap StateOnEvent { get; private set; }
        public EventWrap StateOffEvent { get; private set; }
        public bool State { get; private set; }

        private List<string> _conditions = new List<string>(); 

        public LeastStateCondition()
        {
            StateOnEvent = new EventWrap();
            StateOffEvent = new EventWrap();
        }

        public void AddCondition(string name)
        {
            _conditions.Add(name);
            State = _conditions.Count > 0;
            if (_conditions.Count == 1) StateOnEvent.Dispatch();
        }

        public void RemoveCondition(string name)
        {
            _conditions.Remove(name);
            State = _conditions.Count > 0;
            if (_conditions.Count == 0) StateOnEvent.Dispatch();
        }
    }
}
