using GameModule.StateMachineModule.States;
using SharedModule.ServiceModule.SceneModule;
using SharedModule.StateMachineModule;
using UnityEngine;
using Zenject;

namespace GameModule.StateMachineModule.Installer
{
	public class StateMachineInstaller : MonoInstaller
	{
		[SerializeField] SceneLoadingBackground _settingsLoadingMainScene;
		
		public override void InstallBindings()
		{
			Container.BindInterfacesAndSelfTo<LogicStateMachine<NovelGameState>>().AsSingle();
			
			Container.Bind<NovelStateMachine>().AsSingle();
			
			Container.Bind<StartupState>().AsSingle();
			Container.Bind<LoadMainMenuState>().AsSingle().WithArguments(_settingsLoadingMainScene);

			Debug.Log("here happen");
		}
	}
}
