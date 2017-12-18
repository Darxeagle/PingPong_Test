using System;
using Assets.Common.Scripts.ResourceManagers;
using Assets.Common.Scripts.UnityDI;
using UnityEngine;

namespace Assets.Common.Scripts.Factories
{
    public class PrefabFactory : IPrefabFactory
    {
        [Dependency] public DIContainer DiContainer { set; private get; }
        [Dependency] public IResourceManager ResourceManager { set; private get; }

        public GameObject FromInstance(GameObject prefab)
        {
            var gameObject = GameObject.Instantiate(prefab);
            DiContainer.BuildUp(gameObject);
            return gameObject;
        }

        public GameObject FromResource(string id)
        {
            if (!ResourceManager.Contains(id)) throw new Exception("PrefabFactory: ResourceManager does not contain such key");
            if (!ResourceManager.Loaded(id)) throw new Exception("PrefabFactory: ResourceManager has not loaded this resource yet");
            return FromInstance(ResourceManager.GetResource<GameObject>(id));
        }
    }
}
