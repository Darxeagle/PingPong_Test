using System.Collections;

namespace Assets.Common.Scripts.Commands
{
    public interface ICommand : IEnumerator
    {
        void Execute();
    }
}
