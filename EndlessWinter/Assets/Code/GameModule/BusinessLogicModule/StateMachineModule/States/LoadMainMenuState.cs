using GameModule.ServiceModule.SceneModule;
using SharedModule.ServiceModule.SceneModule;
using Zenject;

namespace GameModule.BusinessLogicModule.StateMachineModule
{
	public class LoadMainMenuState : NovelState
	{
		private readonly SceneLoader _sceneLoader;
		private readonly SceneLoadingImmediately _settingsLoading;

		[Inject]
		public LoadMainMenuState(SceneLoader __sceneLoader, SceneLoadingImmediately __settingsLoading) 
			: base()
		{
			_sceneLoader = __sceneLoader;
			_settingsLoading = __settingsLoading;
		}

		public override async void Enter()
		{
			base.Enter();
			
			await _sceneLoader.LoadSceneImmediately(_settingsLoading);
			
			onNextState?.Invoke(NovelGameState.MainMenu);
		}
	}
}
