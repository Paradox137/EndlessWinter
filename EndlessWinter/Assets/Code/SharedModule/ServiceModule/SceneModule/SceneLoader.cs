using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace SharedModule.ServiceModule.SceneModule
{
	public class SceneLoader
	{
		private BackgroundLoading.Complete complete;
		private ISceneLoaderService _loaderService;
		
		public SceneLoader(ISceneLoaderService __loaderService)
		{
			_loaderService = __loaderService;
		}
		
		public async UniTask LoadSceneBackground(SceneLoadingBackground __backgroundSettings, Action onUploadedData)
		{
			onUploadedData += ActivateScene;
			
			await _loaderService.LoadAsync(__backgroundSettings.ScreenScene);
			
			complete = await _loaderService.LoadInBackground(__backgroundSettings.NecessaryScene);
		}
		
		public async void LoadSceneBackground(SceneLoadingBackground __backgroundSettings, LoadSceneMode __loadSceneMode, float __time)
		{
			await _loaderService.LoadAsync(__backgroundSettings.ScreenScene, __loadSceneMode);
			
			complete = await _loaderService.LoadInBackground(__backgroundSettings.NecessaryScene);

			await UniTask.WaitForSeconds(__time);
			
			ActivateScene();
		}
		
		private async UniTask LoadSceneImmediately(SceneLoadingImmediately __immediatelySettings)
		{
			await _loaderService.LoadAsync(__immediatelySettings.NecessaryScene);
		}

		private void ActivateScene()
		{
			complete.Activate();
		}

		~SceneLoader()
		{
			_loaderService = null;
		}
	}
}
