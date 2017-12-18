using System;
using System.Collections;
using Assets.Common.Scripts.Data;
using Assets.Common.Scripts.Factories;
using Assets.Common.Scripts.UnityDI;
using UnityEngine;

namespace Assets.Common.Scripts.ResourceManagers
{
    class ResourceManager : IResourceManager
    {
        [Dependency] public ICommandFactory CommandFactory { set; private get; }

        private IValueDict<string, string> _resources = new ValueDict<string, string>();
        private IValueDict<string, object> _assets = new ValueDict<string, object>();

        public void AddResource(string id, string path)
        {
            if (Contains(id)) throw new Exception("ResourceManager: already contains such key");
            _resources.Add(id, path);
        }

        public void AddAsset(string id, object asset)
        {
            if (_assets.Contains(id)) throw new Exception("ResourceManager: already contains such asset");
            _assets.Add(id, asset);
        }

        public bool Contains(string id)
        {
            return _resources.Contains(id) || _assets.Contains(id);
        }

        public bool Loaded(string id)
        {
            if (!Contains(id)) throw new Exception("ResourceManager: does not contain such key");
            return _assets.Contains(id);
        }

        public IEnumerator LoadResource(string id)
        {
            if (!Contains(id)) throw new Exception("ResourceManager: does not contain such key");
            if (_assets.Contains(id)) yield break;
            var loadCommand = CommandFactory.LoadResourceCommand(_resources.Get(id));
            yield return loadCommand;
            _assets.Add(id, loadCommand.Asset);
        } 

        public object GetResource(string id)
        {
            if (!Contains(id)) throw new Exception("ResourceManager: does not contain such key");
            if (!_assets.Contains(id)) throw new Exception("ResourceManager: asset is not loaded yet");

            return _assets.Get(id);
        }

        public T GetResource<T>(string id)
        {
            return GetResource(id) is T ? (T) GetResource(id) : default(T);
        }
    }
}
