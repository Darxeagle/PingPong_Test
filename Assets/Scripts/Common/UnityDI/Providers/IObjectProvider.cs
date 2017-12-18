namespace Assets.Common.Scripts.UnityDI.Providers
{
	public interface IObjectProvider<T> where T : class
	{
		T GetObject(DIContainer container);
	}
}
