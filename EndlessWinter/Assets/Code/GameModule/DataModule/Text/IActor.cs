namespace GameModule.DataModule
{
	public interface IActor
	{
		public ActorType ActorName { get; }
		public string GetReplica();
	}
}
