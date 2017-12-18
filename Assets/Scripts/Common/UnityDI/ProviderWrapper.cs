using Assets.Common.Scripts.UnityDI.Providers;

namespace Assets.Common.Scripts.UnityDI
{
	class ProviderWrapper<T> : IProviderWrapper where T:class
	{
		private readonly IObjectProvider<T> _provider;

		public ProviderWrapper(IObjectProvider<T> provider)
		{
			_provider = provider;
		}

		public object GetObject(DIContainer container)
		{
			return _provider.GetObject(container);
		}
	}
}
