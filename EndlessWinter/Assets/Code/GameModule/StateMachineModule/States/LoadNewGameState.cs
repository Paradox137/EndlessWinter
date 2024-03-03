using Dre0Dru.AddressableAssets.Loaders;
using GameModule.DataModule;
using GameModule.PlayerModule;
using GameModule.SettingsModule;
using Newtonsoft.Json;
using SharedModule.ServiceModule.SceneModule;
using SharedModule.UIModule.Signals;
using UnityEngine;
using Zenject;

namespace GameModule.StateMachineModule
{
	public class LoadNewGameState : NovelState
	{
		private readonly SceneLoader _sceneLoader;
		private readonly SceneLoadingImmediately _settingsLoading;
		private readonly NovelLoadService _novelLoadService;

		[Inject]
		public LoadNewGameState(SceneLoader __sceneLoader, SceneLoadingImmediately __settingsLoading, 
			NovelLoadService __novelLoadService) 
			: base()
		{
			_sceneLoader = __sceneLoader;
			_settingsLoading = __settingsLoading;
			_novelLoadService = __novelLoadService;
		}


		public override async void Enter()
		{
			base.Enter();
			
			await _novelLoadService.LoadPack();

			await _novelLoadService.PlaceInStorage();

			await _sceneLoader.LoadSceneImmediately(_settingsLoading);
			
			//onNextState?.Invoke(NovelGameState.InGame);
		}
	}
}
