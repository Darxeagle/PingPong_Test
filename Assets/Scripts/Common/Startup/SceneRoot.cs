using UnityEngine;

namespace Assets.Common.Scripts.Startup
{
    public class SceneRoot : MonoBehaviour
    {
        [SerializeField] private Transform root;
        [SerializeField] private RectTransform canvasRoot;

        public void AddChild(GameObject gameObject)
        {
            var rectTransform = gameObject.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                gameObject.transform.SetParent(canvasRoot, false);
                return;
            }

            var rootTransform = gameObject.GetComponent<Transform>();
            if (rootTransform != null)
            {
                gameObject.transform.SetParent(root, false);
                return;
            }
        }
    }
}
