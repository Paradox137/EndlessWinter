using Zenject;

namespace GameModule.StateMachineModule.States
{
	public class StartupState : NovelState
	{
		[Inject]
		public StartupState(NovelStateMachine __machine) : base(__machine)
		{
			
		}

		public override void Enter()
		{
			base.Enter();
			
			NextState(NovelGameState.LoadMainMenu);
		}
	}
}
