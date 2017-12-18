

using System.Collections;
using Assets.Common.Scripts.UnityDI;

namespace Assets.Common.Scripts.Commands
{
    public class Command : ICommand
    {
        [Dependency] public MonoProvider MonoProvider { set; private get; }

        protected IEnumerator enumerator;

        public Command(IEnumerator enumerator = null)
        {
            this.enumerator = enumerator;
        }

        public void Execute()
        {
            MonoProvider.StartCoroutine(enumerator);
        }

        public bool MoveNext() { return enumerator.MoveNext(); }
        public void Reset() { enumerator.Reset(); }
        public object Current { get { return enumerator.Current; }}
    }
}
