using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace SharedModule.ServiceModule.SceneModule
{
	public interface ISceneLoaderService
	{
		UniTask LoadAsync(SceneReference scene, LoadSceneMode mode = LoadSceneMode.Single);
		
		void Load(SceneReference scene, LoadSceneMode mode = LoadSceneMode.Single);
		
		UniTask<BackgroundLoading.Complete> LoadInBackground(SceneReference scene, LoadSceneMode mode = LoadSceneMode.Single);
	}
}
