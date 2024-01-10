using Dre0Dru.AddressableAssets.Loaders;
using GameModule.DataModule;
using GameModule.PlayerModule;
using GameModule.SettingsModule;
using Newtonsoft.Json;
using SharedModule.ServiceModule.SceneModule;
using UnityEngine;
using Zenject;

namespace GameModule.StateMachineModule
{
	public class LoadNewGameState : NovelState
	{
		private readonly SceneLoader _sceneLoader;
		private readonly SceneLoadingImmediately _settingsLoading;
		private readonly ChapterLoadSettings _chapterLoadSettings;
		private readonly NovelLoadService _novelLoadService;
		
		[Inject]
		public LoadNewGameState(SceneLoader __sceneLoader, SceneLoadingImmediately __settingsLoading,
			ChapterLoadSettings __chapterLoadSettings, NovelLoadService ___novelLoadService) 
			: base()
		{
			_sceneLoader = __sceneLoader;
			_settingsLoading = __settingsLoading;
			_chapterLoadSettings = __chapterLoadSettings;
			_novelLoadService = ___novelLoadService;
		}


		public override async void Enter()
		{
			base.Enter();
			
			IAssetsReferenceLoader<TextAsset> loader = new AssetsReferenceLoader<TextAsset>();  
			
			await _novelLoadService.LoadPack(loader, _chapterLoadSettings);

			TextAsset text = loader.GetAsset(_chapterLoadSettings.TestAssetActor);

			Debug.Log(text.text);
			
			Actor actor = JsonConvert.DeserializeObject<Actor>(text.text);

			if (actor != null)
				Debug.Log(actor.actorName);

			await _sceneLoader.LoadSceneImmediately(_settingsLoading);
			//onNextState?.Invoke(NovelGameState.InGame);
		}
	}
}
