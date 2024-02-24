using GameModule.BusinessLogicModule.PlayerUIActions;
using GameModule.PlayerModule;
using GameModule.SettingsModule;
using GameModule.StorageModule;
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
			NovelStorage novelStorage = new NovelStorage();
			
			NovelLoadService novelLoadService = new NovelLoadService(novelStorage, _chapterLoadSettings);
			MenuAction menuAction = new MenuAction();

			Container.Bind<NovelStorage>().FromInstance(novelStorage).AsSingle().CopyIntoAllSubContainers();
			Container.Bind<MenuAction>().FromInstance(menuAction).AsSingle();
			Container.Bind<NovelLoadService>().FromInstance(novelLoadService).AsSingle();
				
			Container.BindInterfacesAndSelfTo<LogicStateMachine<NovelGameState>>().AsSingle();
			
			Container.Bind<StartupState>().AsSingle();
			Container.Bind<LoadMainMenuState>().AsSingle().WithArguments(_settingsLoadingMainScene);
			Container.Bind<MainMenuState>().AsSingle();
			Container.Bind<LoadNewGameState>().AsSingle().WithArguments(_settingsLoadingNewNovelScene);

			Container.BindInterfacesAndSelfTo<NovelStateMachine>().AsSingle();
		}
	}
}
