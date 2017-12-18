using System;
using System.Collections;
using Assets.Common.Scripts.Commands;
using Assets.Common.Scripts.UnityDI;

namespace Assets.Common.Scripts.Factories
{
    public class CommandFactory : ICommandFactory
    {
        [Dependency] public DIContainer DiContainer { private get; set; }

        public ICommand Create<T>()
        {
            if (!typeof(T).IsAssignableFrom(typeof(ICommand))) throw new Exception("CommandFactory: can not create non command type");
            var command = Activator.CreateInstance<T>();
            DiContainer.BuildUp(command);
            return command as ICommand;
        }

        public Command Command(IEnumerator enumerator)
        {
            var command = new Command(enumerator);
            DiContainer.BuildUp(command);
            return command;
        }

        public LoadResourceCommand LoadResourceCommand(string path)
        {
            var command = new LoadResourceCommand(path);
            DiContainer.BuildUp(command);
            return command;
        }
    }
}
