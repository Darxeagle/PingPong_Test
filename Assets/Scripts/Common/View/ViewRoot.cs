using UnityEngine;

namespace Assets.Common.Scripts.View
{
    class ViewRoot : MonoBehaviour
    {
        public void AddChild(GameObject gameObject)
        {
            gameObject.transform.SetParent(transform, false);
            return;
        }

        public void Clear()
        {
            foreach (Transform child in transform)
            {
                Destroy(child);
            }
        }
    }
}
