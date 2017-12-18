using System.Collections;
using UnityEngine;

namespace Assets.Common.Scripts.View
{
    interface IViewManager
    {
        void AddGameObject(string viewId, GameObject gameObject);
        void AddPrefab(string viewId, GameObject prefab);
        void AddResource(string viewId, string resource);
        bool Contains(string viewId);

        IEnumerator ShowView(string viewId);
        IEnumerator HideView(string viewId);
        IEnumerator LoadView(string viewId);
        IEnumerator InitView(string viewId);
    }
}
