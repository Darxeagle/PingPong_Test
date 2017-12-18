using Assets.Common.Scripts.Events;

namespace Assets.Common.Scripts.Input
{
    public interface IInputController
    {
        EventWrap InputEnabledEvent { get; }
        EventWrap InputDisabledEvent { get; }
        bool InputIsEnabled { get; }

        void AddBlockCondition(string name);
        void RemoveBlockCondition(string name);
    }
}
