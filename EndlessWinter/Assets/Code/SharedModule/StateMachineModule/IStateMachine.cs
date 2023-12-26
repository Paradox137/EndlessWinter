namespace SharedModule.StateMachineModule
{
	public interface IStateMachine<TTrigger>
	{
		void Fire(TTrigger trigger);
	}
}
