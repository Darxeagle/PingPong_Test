using Assets.Common.Scripts.Data;
using Assets.Common.Scripts.Events;

namespace Assets.Common.Scripts.Input
{
    class InputController : IInputController
    {
        private IStateCondition _stateCondition = new LeastStateCondition();

        public EventWrap InputEnabledEvent { get { return _stateCondition.StateOffEvent; } }
        public EventWrap InputDisabledEvent { get { return _stateCondition.StateOnEvent; } }
        public bool InputIsEnabled { get { return !_stateCondition.State; } }

        public void AddBlockCondition(string name)
        {
            _stateCondition.AddCondition(name);
        }
        public void RemoveBlockCondition(string name)
        {
            _stateCondition.RemoveCondition(name);
        }
    }
}
