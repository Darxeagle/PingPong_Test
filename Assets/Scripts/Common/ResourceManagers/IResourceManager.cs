using System.Collections;

namespace Assets.Common.Scripts.ResourceManagers
{
    public interface IResourceManager
    {
        void AddResource(string id, string path);
        void AddAsset(string id, object asset);
        bool Contains(string id);
        bool Loaded(string id);
        IEnumerator LoadResource(string id);
        object GetResource(string id);
        T GetResource<T>(string id);
    }
}
