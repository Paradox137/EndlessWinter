using GameModule.BusinessLogicModule.PlayerUIActions;
using SharedModule.ServiceModule.SceneModule;
using SharedModule.StateMachineModule;
using UnityEngine;
using Zenject;

namespace GameModule.StateMachineModule
{
	public class StateMachineInstaller : MonoInstaller
	{
		[SerializeField] SceneLoadingImmediately _settingsLoadingMainScene;
		[SerializeField] SceneLoadingImmediately _settingsLoadingNewNovelScene;
		
		public override void InstallBindings()
		{
			MenuAction menuAction = new MenuAction();
			Container.Bind<MenuAction>().FromInstance(menuAction).AsSingle();
			
			Container.BindInterfacesAndSelfTo<LogicStateMachine<NovelGameState>>().AsSingle();
			
			Container.Bind<StartupState>().AsSingle();
			Container.Bind<LoadMainMenuState>().AsSingle().WithArguments(_settingsLoadingMainScene);
			Container.Bind<MainMenuState>().AsSingle();
			Container.Bind<LoadNewGameState>().AsSingle().WithArguments(_settingsLoadingNewNovelScene);

			Container.BindInterfacesAndSelfTo<NovelStateMachine>().AsSingle();
		}
	}
}
