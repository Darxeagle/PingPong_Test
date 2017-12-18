using UnityEngine;

namespace Assets.Common.Scripts.Factories
{
    public interface IPrefabFactory
    {
        GameObject FromInstance(GameObject prefab);
        GameObject FromResource(string id);
    }
}
