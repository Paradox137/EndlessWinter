using GameModule.StateMachineModule;
using UnityEngine;
using Zenject;

namespace GameModule.BusinessLogicModule
{
	public class AppStartup : MonoBehaviour
	{
		[Inject]
		private NovelStateMachine _machine;
		
		private void Start()
		{
			_machine.Fire(NovelGameState.Startup);
		}
	}
}
