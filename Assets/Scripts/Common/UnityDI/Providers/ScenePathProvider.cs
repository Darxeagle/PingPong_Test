using System.Linq;
using Assets.Common.Scripts.UnityDI.Finders;
using UnityEngine;

namespace Assets.Common.Scripts.UnityDI.Providers
{
	public class ScenePathProvider<T> : IObjectProvider<T> where T:class
	{
        private bool _inited;
        private readonly string _path;

		public ScenePathProvider(string path)
		{
			_path = path;
        }

        public T GetObject(DIContainer container)
		{
			var gameObject = new MaskFinder().Find(_path);
			if (gameObject == null)
				throw new ContainerException("Can't find game object \"" + _path + "\"");

            if (!_inited)
            {
                container.BuildUp(gameObject);
                _inited = true;
            }

            if (typeof (T) == typeof (GameObject))
			{
				return (T)(object)gameObject;
			}
			
			if (typeof (T) == typeof (Transform))
			{
				return (T)(object)gameObject.transform;
			}

			T component = gameObject.GetComponents<Component>().OfType<T>().FirstOrDefault();
            if (component != null)
				return component;

			throw new ContainerException("Can't find component \"" + typeof(T).FullName + "\" of game object \"" + _path + "\"");
		}

	    public void OnInjected()
	    {
	    }
	}
}
