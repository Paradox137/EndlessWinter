using GameModule.BusinessLogicModule.PlayerUIActions;
using GameModule.PlayerModule;
using GameModule.SettingsModule;
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
		[SerializeField] ChapterLoadSettings _chapterLoadSettings;
		
		public override void InstallBindings()
		{
			NovelLoadService novelLoadService = new NovelLoadService();
			MenuAction menuAction = new MenuAction();
			Container.Bind<MenuAction>().FromInstance(menuAction).AsSingle();
			Container.Bind<NovelLoadService>().FromInstance(novelLoadService).AsSingle();
				
			Container.BindInterfacesAndSelfTo<LogicStateMachine<NovelGameState>>().AsSingle();
			
			Container.Bind<StartupState>().AsSingle();
			Container.Bind<LoadMainMenuState>().AsSingle().WithArguments(_settingsLoadingMainScene);
			Container.Bind<MainMenuState>().AsSingle();
			Container.Bind<LoadNewGameState>().AsSingle().WithArguments(_settingsLoadingNewNovelScene,_chapterLoadSettings);

			Container.BindInterfacesAndSelfTo<NovelStateMachine>().AsSingle();
		}
	}
}
