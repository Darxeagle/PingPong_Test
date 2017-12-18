using Assets.Common.Scripts.Commands;

namespace Assets.Common.Scripts.View
{
    public interface ITransitionScreen
    {
        ICommand Show(bool instant = false);
        ICommand Hide(bool instant = false);
    }
}
