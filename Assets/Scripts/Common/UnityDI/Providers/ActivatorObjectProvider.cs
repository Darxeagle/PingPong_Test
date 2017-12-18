using System;

namespace Assets.Common.Scripts.UnityDI.Providers
{
	public class ActivatorObjectProvider<T> : IObjectProvider<T> where T : class, new()
	{
		public T GetObject(DIContainer container)
		{
			var obj = Activator.CreateInstance<T>();
			container.BuildUp(obj);
			return obj;
		}
	}
}
