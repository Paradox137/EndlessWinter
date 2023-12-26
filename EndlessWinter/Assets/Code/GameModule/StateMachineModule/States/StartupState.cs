using Zenject;

namespace GameModule.StateMachineModule.States
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
