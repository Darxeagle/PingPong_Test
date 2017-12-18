using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Common.Scripts.Factories;
using Assets.Common.Scripts.ResourceManagers;
using Assets.Common.Scripts.Startup;
using Assets.Common.Scripts.UnityDI;
using UnityEngine;

namespace Assets.Common.Scripts.View
{
    class ViewManager : IViewManager
    {
        [Dependency] public IPrefabFactory PrefabFactory { get; private set; }
        [Dependency] public IResourceManager ResourceManager { get; private set; }
        [Dependency] public SceneRoot SceneRoot { get; private set; }

        private Dictionary<string, GameObject> _gameObjects = new Dictionary<string, GameObject>();


        public void AddGameObject(string viewId, GameObject gameObject)
        {
            if (Contains(viewId)) throw new Exception("ViewManager: already contains such key");
            _gameObjects.Add(viewId, gameObject);
        }

        public void AddPrefab(string viewId, GameObject prefab)
        {
            if (Contains(viewId)) throw new Exception("ViewManager: already contains such key");
            ResourceManager.AddAsset(viewId, prefab);
        }

        public void AddResource(string viewId, string resource)
        {
            if (Contains(viewId)) throw new Exception("ViewManager: already contains such key");
            ResourceManager.AddResource(viewId, resource);
        }


        public bool Contains(string viewId)
        {
            return _gameObjects.ContainsKey(viewId) || ResourceManager.Contains(viewId);
        }

        public IEnumerator ShowView(string viewId)
        {
            if (!Contains(viewId)) throw new Exception("ViewManager: does not contain such key");
            if (_gameObjects.ContainsKey(viewId))
            {
                _gameObjects[viewId].SetActive(true);
                yield break;
            }
            yield return LoadView(viewId);
            yield return InitView(viewId);
            _gameObjects[viewId].SetActive(true);
        } 

        public IEnumerator HideView(string viewId)
        {
            if (!Contains(viewId)) throw new Exception("ViewManager: does not contain such key");
            if (!_gameObjects.ContainsKey(viewId)) throw new Exception("ViewManager: has not instanciated this object yet");
            _gameObjects[viewId].SetActive(false);
            yield return null;
        }

        public IEnumerator LoadView(string viewId)
        {
            if (!Contains(viewId)) throw new Exception("ViewManager: does not contain such key");
            if (_gameObjects.ContainsKey(viewId)) yield break;
            if (ResourceManager.Loaded(viewId)) yield break;
            yield return ResourceManager.LoadResource(viewId);
        }

        public IEnumerator InitView(string viewId)
        {
            if (!Contains(viewId)) throw new Exception("ViewManager: does not contain such key");
            if (_gameObjects.ContainsKey(viewId)) yield break;
            if (!ResourceManager.Loaded(viewId)) throw new Exception("ViewManager: has not loaded this object yet");

            var gameObject = PrefabFactory.FromResource(viewId);
            gameObject.SetActive(false);
            SceneRoot.AddChild(gameObject);

            _gameObjects.Add(viewId, gameObject);
        }
    }
}
