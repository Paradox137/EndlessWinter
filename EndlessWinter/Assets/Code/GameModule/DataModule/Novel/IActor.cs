namespace GameModule.DataModule.Novel
{
	public interface IActor
	{
		public ActorType ActorName { get; }
		public string GetReplica();
	}
}
