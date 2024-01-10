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
		private readonly MainMenuState _mainMenu;
		private readonly LoadNewGameState _loadNewNovel;

		[Inject]
		public NovelStateMachine(LogicStateMachine<NovelGameState> __machine, StartupState __startup,
			LoadMainMenuState __loadMainMenu, MainMenuState __mainMenu, LoadNewGameState __loadNewNovel)
		{
			_machine = __machine;
			
			_startupState = __startup;
			_loadMainMenu = __loadMainMenu;
			_mainMenu = __mainMenu;
			_loadNewNovel = __loadNewNovel;
		}
		
		public void Initialize()
		{
			Subscribe();
			
			_machine.DefineState(() => _loadMainMenu);
			_machine.DefineState(() => _startupState);
			_machine.DefineState(() => _mainMenu);
			_machine.DefineState(() => _loadNewNovel);
			
			_machine.DefineStartTransition<StartupState>(NovelGameState.Startup);
			
			_machine.DefineTransition<StartupState, LoadMainMenuState>(NovelGameState.LoadMainMenu);
			_machine.DefineTransition<LoadMainMenuState, MainMenuState>(NovelGameState.MainMenu);
			_machine.DefineTransition<MainMenuState, LoadNewGameState>(NovelGameState.LoadNewGame);
		}

		private void Subscribe()
		{
			List<NovelState> _novelStates = new List<NovelState>()
			{
				_startupState,
				_loadMainMenu,
				_mainMenu,
				_loadNewNovel,
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
