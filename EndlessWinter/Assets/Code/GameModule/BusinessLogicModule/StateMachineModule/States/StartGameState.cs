using Cysharp.Threading.Tasks;
using GameModule.BusinessLogicModule.PlayerUIActions;
using GameModule.ServiceModule.InGameModule;
using GameModule.ServiceModule.SaveLoadModule;
using GameModule.UIModule.Window;
using SharedModule.CollectionModule;
using Zenject;

namespace GameModule.BusinessLogicModule.StateMachineModule
{
	public class StartGameState : NovelState
	{
		private readonly NovelFlowController _novelFlowController;
		private readonly SignalBus _signalBus;
		private readonly NovelLoadService _novelLoadService;
		private readonly PlayerSaveLoadSystem _saveLoadSystem;
		private readonly InGameAction _inGameAction;


		private (ushort, ushort, ushort) _nextSave;
		[Inject]
		public StartGameState(NovelFlowController __novelFlowController, SignalBus __signalBus, NovelLoadService __novelLoadService, 
			PlayerSaveLoadSystem __saveLoadSystem, InGameAction __inGameAction) 
			: base()
		{
			_novelFlowController = __novelFlowController;
			_signalBus = __signalBus;
			_novelLoadService = __novelLoadService;
			_saveLoadSystem = __saveLoadSystem;
			_inGameAction = __inGameAction;
		}
		
		private async void LoadNextChapter()
		{
			_saveLoadSystem.GetPlayerData().SavePlace = _nextSave;
			_saveLoadSystem.Save();
			
			await _novelLoadService.PlaceDataInStorage(_saveLoadSystem.GetPlayerData().SavePlace);
			
			_novelFlowController.InitNovelData();
		}

		public override async void Enter()
		{
			base.Enter();
			
			_inGameAction.Action += OnPlayerAction;
			
			_novelFlowController.InitNovelData();
			
			_novelFlowController.onEndGame += EndGame;
			
			await TryPreloadNextChapter();
		}
		private void OnPlayerAction(InGameLogicItem __gameLogic)
		{
			if (__gameLogic == InGameLogicItem.BackToMainMenu)
			{
				_novelFlowController.Dispose();
				onNextState?.Invoke(NovelGameState.LoadMainMenu);
			}
		}

		private async UniTask TryPreloadNextChapter()
		{
			ushort nexChapter = (ushort)(_saveLoadSystem.GetPlayerData().SavePlace.ChapterSavedPart + 1);

			_nextSave = (nexChapter, 0, 0);

			if (_nextSave.Item1 > 1)
				return;

			_novelFlowController.onNextChapter += LoadNextChapter;

			_novelLoadService.PreloadChapterData(_nextSave);

			await _novelLoadService.LoadChapterData(_nextSave);
		}

		public override void Exit()
		{
			base.Exit();
			
			_inGameAction.Action -= OnPlayerAction;
		}
		private void EndGame()
		{
			WindowsCollection.Get<GameEndWindow>().Show();
		}
	}
}
