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
		[Inject]
		private NovelStateMachine _machine;
		
		private void Start()
		{
			_machine.Fire(NovelGameState.Startup);
		}
	}
}
