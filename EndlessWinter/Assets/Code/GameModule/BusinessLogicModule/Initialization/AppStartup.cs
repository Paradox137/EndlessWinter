using SharedModule.ServiceModule.SceneModule;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace GameModule.BusinessLogicModule
{
	public class AppStartup : MonoBehaviour
	{
		[SerializeField] SceneLoadingBackground _settingsLoading;
		private SceneLoader _sceneLoader;

		[Inject]
		public void Construct(SceneLoader __sceneLoader)
		{
			_sceneLoader = __sceneLoader;
		}

		public void Start()
		{
			_sceneLoader.LoadSceneBackground(_settingsLoading, LoadSceneMode.Additive, 2f);
		}
	}
}
