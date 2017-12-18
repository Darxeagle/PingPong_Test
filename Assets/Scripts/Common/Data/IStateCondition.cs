using Assets.Common.Scripts.Events;

namespace Assets.Common.Scripts.Data
{
    interface IStateCondition
    {
        EventWrap StateOnEvent { get; }
        EventWrap StateOffEvent { get; }
        bool State { get; }
        void AddCondition(string name);
        void RemoveCondition(string name);
    }
}
