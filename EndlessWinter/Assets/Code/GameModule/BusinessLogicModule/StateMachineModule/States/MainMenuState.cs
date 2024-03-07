using System.Threading;
using GameModule.BusinessLogicModule.PlayerUIActions;
using GameModule.ServiceModule.SaveLoadModule;
using GameModule.UIModule.Window;
using SharedModule.CollectionModule;
using Zenject;

namespace GameModule.BusinessLogicModule.StateMachineModule
{
	public class MainMenuState : NovelState
	{
		private readonly PlayerSaveLoadSystem _saveLoadSystem;
		private readonly MenuAction _gameStartAction;
		private readonly NovelLoadService _novelLoadService;

		private CancellationTokenSource _cancellationTokenSource;
		[Inject]
		public MainMenuState(PlayerSaveLoadSystem __saveLoadSystem,  MenuAction __gameStartAction, NovelLoadService __novelLoadService) : base()
		{
			_saveLoadSystem = __saveLoadSystem;
			_gameStartAction = __gameStartAction;
			_novelLoadService = __novelLoadService;

			_cancellationTokenSource = new CancellationTokenSource();
		}

		public override void Enter()
		{
			base.Enter();
			
			_gameStartAction.Action += ChooseGame;
			
			WindowsCollection.Get<MainMenuWindow>().Show(_saveLoadSystem.GetPlayerData().IsGameExists);
			
			_novelLoadService.PreloadChapterData(_saveLoadSystem.GetPlayerData().SavePlace, _cancellationTokenSource.Token);
		}
		
		private void ChooseGame(MenuLogicAction __item)
		{
			_cancellationTokenSource.Cancel();
			_cancellationTokenSource = new CancellationTokenSource();
			
			_saveLoadSystem.GetPlayerData().IsGameExists = true;
			_saveLoadSystem.Save();
			
			switch (__item)
			{
				case MenuLogicAction.NewGame:
					_saveLoadSystem.ResetData();
					_saveLoadSystem.Save();
					_novelLoadService.PreloadChapterData(_saveLoadSystem.GetPlayerData().SavePlace, _cancellationTokenSource.Token);
					onNextState?.Invoke(NovelGameState.LoadNewGame);
					break;
				case MenuLogicAction.ContinueGame:
					onNextState?.Invoke(NovelGameState.LoadSavedGame);
					break;
			}
		}
		
		public override void Exit()
		{
			_gameStartAction.Action -= ChooseGame;
		}
	}
}
