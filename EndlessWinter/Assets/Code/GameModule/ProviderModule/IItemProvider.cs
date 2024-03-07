namespace GameModule.ProviderModule
{
	public interface IItemProvider<T>
	{
		public T GetItem();
	}
}
