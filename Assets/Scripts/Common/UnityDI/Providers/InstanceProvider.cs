namespace Assets.Common.Scripts.UnityDI.Providers
{
	public class InstanceProvider<T> : IObjectProvider<T> where T : class
	{
        private bool _inited;
        private readonly T _instance;

		public InstanceProvider(T instance)
		{
			_instance = instance;
		}

		public T GetObject(DIContainer container)
		{
		    if (!_inited)
		    {
		        container.BuildUp(_instance);
		        _inited = true;
		    }

			return _instance;
		}
	}
}
