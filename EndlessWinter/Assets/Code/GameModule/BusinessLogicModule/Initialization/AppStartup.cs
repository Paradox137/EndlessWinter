using System;
using GameModule.StateMachineModule;
using SharedModule.ServiceModule.SceneModule;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace GameModule.BusinessLogicModule
{
	public class AppStartup : MonoBehaviour
	{
		private NovelStateMachine _machine;

		[Inject]
		public void Construct(NovelStateMachine __machine)
		{
			_machine = __machine;
		}

		private void Start()
		{
			_machine.Fire(NovelGameState.Startup);
		}
	}
}
