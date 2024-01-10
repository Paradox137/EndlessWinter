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
		private readonly PlayerSaveLoadSystem _saveLoadSystem;
		private readonly MenuAction _gameStartAction;
		
		[Inject]
		public MainMenuState(PlayerSaveLoadSystem __saveLoadSystem,  MenuAction __gameStartAction) : base()
		{
			_saveLoadSystem = __saveLoadSystem;
			_gameStartAction = __gameStartAction;
		}

		public override void Enter()
		{
			base.Enter();

			_gameStartAction.Action += ChooseGame;
			
			WindowsCollection.Get<MainMenuWindow>().Show(_saveLoadSystem.GetPlayerData().IsGameStarted);
		}
		
		private void ChooseGame(MenuLogicAction __item)
		{
			Debug.Log("Choose " + __item);
			
			_saveLoadSystem.PlayerData.IsGameStarted = true;
			_saveLoadSystem.Save();
			
			onNextState?.Invoke(NovelGameState.LoadNewGame);
		}
		
		public override void Exit()
		{
			_gameStartAction.Action -= ChooseGame;
		}
	}
}
