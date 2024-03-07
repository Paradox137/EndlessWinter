using System.Threading;
using GameModule.ServiceModule.SaveLoadModule;
using GameModule.ServiceModule.SceneModule;
using SharedModule.ServiceModule.SceneModule;
using Zenject;

namespace GameModule.BusinessLogicModule.StateMachineModule
{
	public class LoadNewGameState : NovelState
	{
		private readonly SceneLoader _sceneLoader;
		private readonly SceneLoadingImmediately _settingsLoading;
		private readonly NovelLoadService _novelLoadService;
		private readonly PlayerSaveLoadSystem _playerSaveLoadSystem;

		private CancellationTokenSource _cancellationTokenSource;
		[Inject]
		public LoadNewGameState(SceneLoader __sceneLoader, SceneLoadingImmediately __settingsLoading, 
			NovelLoadService __novelLoadService, PlayerSaveLoadSystem __playerSaveLoadSystem) 
			: base()
		{
			_sceneLoader = __sceneLoader;
			_settingsLoading = __settingsLoading;
			_novelLoadService = __novelLoadService;
			_playerSaveLoadSystem = __playerSaveLoadSystem;

			_cancellationTokenSource = new CancellationTokenSource();
		}


		public override async void Enter()
		{
			base.Enter();

			await _novelLoadService.LoadChapterData(_playerSaveLoadSystem.GetPlayerData().SavePlace, _cancellationTokenSource.Token);

			await _novelLoadService.PlaceDataInStorage(_playerSaveLoadSystem.GetPlayerData().SavePlace);

			await _sceneLoader.LoadSceneImmediately(_settingsLoading);
			
			onNextState?.Invoke(NovelGameState.StartGame);
		}
	}
}
