using System.Collections;
using Assets.Common.Scripts.Commands;

namespace Assets.Common.Scripts.Factories
{
    interface ICommandFactory
    {
        ICommand Create<T>();
        Command Command(IEnumerator enumerator);
        LoadResourceCommand LoadResourceCommand(string path);
    }
}
