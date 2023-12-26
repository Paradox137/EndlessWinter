using SharedModule.ServiceModule.SceneModule;
using UnityEngine.SceneManagement;
using Zenject;

namespace GameModule.StateMachineModule.States
{
	public class LoadMainMenuState : NovelState
	{
		private readonly SceneLoader _sceneLoader;
		private readonly SceneLoadingBackground _settingsLoading;

		[Inject]
		public LoadMainMenuState(NovelStateMachine __machine, SceneLoader __sceneLoader, SceneLoadingBackground __settingsLoading) 
			: base(__machine)
		{
			_sceneLoader = __sceneLoader;
			_settingsLoading = __settingsLoading;
		}

		public override void Enter()
		{
			base.Enter();
			
			_sceneLoader.LoadSceneBackground(_settingsLoading, LoadSceneMode.Additive, 2f);
		}
	}
}
