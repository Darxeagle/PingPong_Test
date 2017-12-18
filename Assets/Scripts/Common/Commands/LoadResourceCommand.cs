using System.Collections;
using UnityEngine;

namespace Assets.Common.Scripts.Commands
{
    public class LoadResourceCommand : Command
    {
        public LoadResourceCommand(string path)
        {
            enumerator = LoadResource(path);
        }

        IEnumerator LoadResource(string path)
        {
            var request = Resources.LoadAsync(path);
            while (!request.isDone)
            {
                Progress = request.progress;
                yield return null;
            }
            Progress = 1f;
            Asset = request.asset;
        }

        public float Progress { get; private set; }
        public object Asset { get; private set; }
    }
}
