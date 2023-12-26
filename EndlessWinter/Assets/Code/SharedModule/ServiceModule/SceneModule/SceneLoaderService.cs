using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SharedModule.ServiceModule.SceneModule
{
	public class SceneLoaderService : ISceneLoaderService
	{
		public UniTask LoadAsync(SceneReference scene, LoadSceneMode mode = LoadSceneMode.Single)
		{
			AsyncOperation operation = SceneManager.LoadSceneAsync(scene.SceneName, mode);
			
			return operation.ToUniTask();
		}
		
		public void Load(SceneReference scene, LoadSceneMode mode = LoadSceneMode.Single)
		{
			SceneManager.LoadScene(scene.SceneName, mode);
		}
		
		public UniTask<BackgroundLoading.Complete> LoadInBackground(SceneReference scene, LoadSceneMode mode = LoadSceneMode.Single)
		{
			BackgroundLoading background = new BackgroundLoading(scene.SceneName, mode);

			return background.Load();
		}
	}
}
