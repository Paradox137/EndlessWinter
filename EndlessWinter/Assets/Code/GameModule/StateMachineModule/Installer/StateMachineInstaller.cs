using SharedModule.ServiceModule.SceneModule;
using SharedModule.StateMachineModule;
using UnityEngine;
using Zenject;

namespace GameModule.StateMachineModule
{
	public class StateMachineInstaller : MonoInstaller
	{
		[SerializeField] SceneLoadingImmediately _settingsLoadingMainScene;
		
		public override void InstallBindings()
		{
			Container.BindInterfacesAndSelfTo<LogicStateMachine<NovelGameState>>().AsSingle();
			
			Container.Bind<StartupState>().AsSingle();
			Container.Bind<LoadMainMenuState>().AsSingle().WithArguments(_settingsLoadingMainScene);
			Container.Bind<MainMenuState>().AsSingle();

			Container.BindInterfacesAndSelfTo<NovelStateMachine>().AsSingle();
		}
	}
}
