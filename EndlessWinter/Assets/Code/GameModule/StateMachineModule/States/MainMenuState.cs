using System;
using Cysharp.Threading.Tasks;
using GameModule.BusinessLogicModule.PlayerUIActions;
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
		private readonly MenuAction _menuAction;
		
		[Inject]
		public MainMenuState(SaveLoadSystem __saveLoadSystem,  MenuAction __menuAction) : base()
		{
			_saveLoadSystem = __saveLoadSystem;
			_menuAction = __menuAction;
		}

		public override void Enter()
		{
			base.Enter();

			_menuAction.Action += ChooseGame;
			
			WindowsCollection.Get<MainMenuWindow>().Show(_saveLoadSystem.GetPlayerData().IsGameStarted);
		}
		
		private void ChooseGame(MenuLogicAction __item)
		{
			Debug.Log("Choose " + __item);

			PlayerData pd = _saveLoadSystem.GetPlayerData();
			pd.IsGameStarted = true;
			_saveLoadSystem.Save(pd);
		}
		
		public override void Exit()
		{
			_menuAction.Action -= ChooseGame;
		}
	}
}
