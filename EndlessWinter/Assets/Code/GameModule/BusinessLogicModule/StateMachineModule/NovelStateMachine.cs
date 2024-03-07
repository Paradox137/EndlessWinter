using System.Collections.Generic;
using SharedModule.StateMachineModule;
using Zenject;

namespace GameModule.BusinessLogicModule.StateMachineModule
{
	public class NovelStateMachine : IStateMachine<NovelGameState>, IInitializable
	{
		private readonly LogicStateMachine<NovelGameState> _machine;
		
		private readonly StartupState _startupState;
		private readonly LoadMainMenuState _loadMainMenu;
		private readonly MainMenuState _mainMenu;
		private readonly LoadNewGameState _loadNewNovel;
		private readonly LoadSavedGameState _loadSavedNovel;
		private readonly StartGameState _startGame;

		[Inject]
		public NovelStateMachine(LogicStateMachine<NovelGameState> __machine, StartupState __startup,
			LoadMainMenuState __loadMainMenu, MainMenuState __mainMenu, LoadNewGameState __loadNewNovel, 
			LoadSavedGameState __loadSavedNovel, StartGameState __startGame)
		{
			_machine = __machine;
			
			_startupState = __startup;
			_loadMainMenu = __loadMainMenu;
			_mainMenu = __mainMenu;
			_loadNewNovel = __loadNewNovel;
			_loadSavedNovel = __loadSavedNovel;
			_startGame = __startGame;
		}
		
		public void Initialize()
		{
			Subscribe();
			
			_machine.DefineState(() => _loadMainMenu);
			_machine.DefineState(() => _startupState);
			_machine.DefineState(() => _mainMenu);
			_machine.DefineState(() => _loadNewNovel);
			_machine.DefineState(() => _loadSavedNovel);
			_machine.DefineState(() => _startGame);
			
			_machine.DefineStartTransition<StartupState>(NovelGameState.Startup);
			
			_machine.DefineTransition<StartupState, LoadMainMenuState>(NovelGameState.LoadMainMenu);
			_machine.DefineTransition<LoadMainMenuState, MainMenuState>(NovelGameState.MainMenu);
			_machine.DefineTransition<MainMenuState, LoadNewGameState>(NovelGameState.LoadNewGame);
			_machine.DefineTransition<MainMenuState, LoadSavedGameState>(NovelGameState.LoadSavedGame);
			_machine.DefineTransition<LoadSavedGameState, StartGameState>(NovelGameState.StartGame);
			_machine.DefineTransition<LoadNewGameState, StartGameState>(NovelGameState.StartGame);
			
			_machine.DefineTransition<StartGameState, LoadMainMenuState>(NovelGameState.LoadMainMenu);
		}

		private void Subscribe()
		{
			List<NovelState> _novelStates = new List<NovelState>()
			{
				_startupState,
				_loadMainMenu,
				_mainMenu,
				_loadNewNovel,
				_loadSavedNovel,
				_startGame
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
