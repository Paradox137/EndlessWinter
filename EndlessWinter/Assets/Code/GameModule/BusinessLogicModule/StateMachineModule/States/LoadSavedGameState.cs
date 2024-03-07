using System.Threading;
using GameModule.ServiceModule.InGameModule;
using GameModule.ServiceModule.SaveLoadModule;
using GameModule.ServiceModule.SceneModule;
using SharedModule.ServiceModule.SceneModule;
using Zenject;

namespace GameModule.BusinessLogicModule.StateMachineModule
{
	public class LoadSavedGameState : NovelState
	{
		private readonly SceneLoader _sceneLoader;
		private readonly SceneLoadingImmediately _settingsLoading;
		private readonly NovelLoadService _novelLoadService;
		private readonly PlayerSaveLoadSystem _playerSaveLoadSystem;
		private readonly NovelFlowController _novelFlowController;

		private readonly CancellationTokenSource _cancellationTokenSource;
		[Inject]
		public LoadSavedGameState(SceneLoader __sceneLoader, SceneLoadingImmediately __settingsLoading, 
			NovelLoadService __novelLoadService, PlayerSaveLoadSystem __playerSaveLoadSystem, NovelFlowController __novelFlowController) 
			: base()
		{
			_sceneLoader = __sceneLoader;
			_settingsLoading = __settingsLoading;
			_novelLoadService = __novelLoadService;
			_playerSaveLoadSystem = __playerSaveLoadSystem;
			_novelFlowController = __novelFlowController;

			_cancellationTokenSource = new CancellationTokenSource();
		}


		public override async void Enter()
		{
			base.Enter();
			
			await _novelLoadService.LoadChapterData(_playerSaveLoadSystem.GetPlayerData().SavePlace, _cancellationTokenSource.Token);

			await _novelLoadService.PlaceDataInStorage(_playerSaveLoadSystem.GetPlayerData().SavePlace);

			_novelFlowController.SkipFlowUntilSavePlace();
			
			await _sceneLoader.LoadSceneImmediately(_settingsLoading);
			
			onNextState?.Invoke(NovelGameState.StartGame);
		}
	}
}
