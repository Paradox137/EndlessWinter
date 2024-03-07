using GameModule.BusinessLogicModule.PlayerUIActions;
using GameModule.ConfigsModule;
using GameModule.ServiceModule.InGameModule;
using GameModule.ServiceModule.SaveLoadModule;
using GameModule.StorageModule;
using SharedModule.ServiceModule.SceneModule;
using SharedModule.StateMachineModule;
using UnityEngine;
using Zenject;

namespace GameModule.BusinessLogicModule.StateMachineModule
{
	public class StateMachineInstaller : MonoInstaller
	{
		[SerializeField] SceneLoadingImmediately _settingsLoadingMainScene;
		[SerializeField] SceneLoadingImmediately _settingsLoadingNewNovelScene;
		[SerializeField] ChapterLoadConfig _chapterLoadConfigs;
		
		public override void InstallBindings()
		{
			NovelStorage novelStorage = new NovelStorage();
			Container.Bind<NovelFlowController>().AsSingle();
			//NovelFlowController novelFlowController = new NovelFlowController(novelStorage);
			
			NovelLoadService novelLoadService = new NovelLoadService(novelStorage, _chapterLoadConfigs);
			MenuAction menuAction = new MenuAction();
			InGameAction inGameAction = new InGameAction();

			Container.Bind<NovelStorage>().FromInstance(novelStorage).AsSingle().CopyIntoAllSubContainers();
			
			Container.Bind<MenuAction>().FromInstance(menuAction).AsCached();
			Container.Bind<InGameAction>().FromInstance(inGameAction).AsSingle();
			
			Container.Bind<NovelLoadService>().FromInstance(novelLoadService).AsSingle();
				
			Container.BindInterfacesAndSelfTo<LogicStateMachine<NovelGameState>>().AsSingle();
			
			Container.Bind<StartupState>().AsSingle();
			Container.Bind<LoadMainMenuState>().AsSingle().WithArguments(_settingsLoadingMainScene);
			Container.Bind<MainMenuState>().AsSingle();
			Container.Bind<LoadNewGameState>().AsSingle().WithArguments(_settingsLoadingNewNovelScene);
			Container.Bind<LoadSavedGameState>().AsSingle().WithArguments(_settingsLoadingNewNovelScene);
			Container.Bind<StartGameState>().AsSingle();

			Container.BindInterfacesAndSelfTo<NovelStateMachine>().AsSingle();
		}
	}
}
