using GameModule.StateMachineModule.States;
using SharedModule.StateMachineModule;
using Zenject;

namespace GameModule.StateMachineModule
{
	public class NovelStateMachine : IStateMachine<NovelGameState>, IInitializable
	{
		private readonly LogicStateMachine<NovelGameState> machine;
		
		private readonly StartupState _startupState;
		private readonly LoadMainMenuState _loadMainMenu;

		[Inject]
		public NovelStateMachine(StartupState __startup, LoadMainMenuState __loadMainMenu)
		{
			_startupState = __startup;
			_loadMainMenu = __loadMainMenu;
		}
		
		public void Initialize()
		{
			machine.DefineState(() => _loadMainMenu);
			machine.DefineState(() => _startupState);
			
			machine.DefineStartTransition<StartupState>(NovelGameState.Startup);
			
			machine.DefineTransition<StartupState, LoadMainMenuState>(NovelGameState.LoadMainMenu);
		}
		
		public void Fire(NovelGameState trigger)
		{
			machine.Fire(trigger);
		}
	}
}
