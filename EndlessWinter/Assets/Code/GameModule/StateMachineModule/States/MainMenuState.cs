using GameModule.PlayerModule;
using GameModule.UIModule.Window;
using SharedModule.UIModule.Window;
using UnityEngine;
using Zenject;

namespace GameModule.StateMachineModule
{
	public class MainMenuState : NovelState
	{
		private readonly SaveLoadSystem _saveLoadSystem;
		
		[Inject]
		public MainMenuState(SaveLoadSystem __saveLoadSystem) : base()
		{
			_saveLoadSystem = __saveLoadSystem;
		}

		public override void Enter()
		{
			base.Enter();
			
			WindowsCollection.Get<MainMenuWindow>().Show(_saveLoadSystem.PlayerData.IsGameStarted);
		}
	}
}
