using System.Collections.Generic;
using SharedModule.StateMachineModule;
using Zenject;

namespace GameModule.StateMachineModule
{
	public class NovelStateMachine : IStateMachine<NovelGameState>, IInitializable
	{
		private readonly LogicStateMachine<NovelGameState> _machine;
		
		private readonly StartupState _startupState;
		private readonly LoadMainMenuState _loadMainMenu;

		[Inject]
		public NovelStateMachine(LogicStateMachine<NovelGameState> __machine, StartupState __startup, LoadMainMenuState __loadMainMenu)
		{
			_machine = __machine;
			
			_startupState = __startup;
			_loadMainMenu = __loadMainMenu;
		}
		
		public void Initialize()
		{
			Subscribe();
			
			_machine.DefineState(() => _loadMainMenu);
			_machine.DefineState(() => _startupState);
			
			_machine.DefineStartTransition<StartupState>(NovelGameState.Startup);
			
			_machine.DefineTransition<StartupState, LoadMainMenuState>(NovelGameState.LoadMainMenu);
		}

		private void Subscribe()
		{
			List<NovelState> _novelStates = new List<NovelState>()
			{
				_startupState,
				_loadMainMenu,
			};
			
			foreach (NovelState state in _novelStates)
			{
				state.onNextState += Fire;
			}
		}

		public void Fire(NovelGameState trigger)
		{
			_machine.Fire(trigger);
		}
	}
}
