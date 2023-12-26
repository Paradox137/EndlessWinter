using Zenject;

namespace GameModule.StateMachineModule
{
	public class StartupState : NovelState
	{
		[Inject]
		public StartupState() 
			: base()
		{
			
		}


		public override void Enter()
		{
			base.Enter();
			
			onNextState?.Invoke(NovelGameState.LoadMainMenu);
		}
	}
}
