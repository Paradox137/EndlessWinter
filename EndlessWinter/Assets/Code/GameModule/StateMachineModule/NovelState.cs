using System;
using SharedModule.StateMachineModule;
using UnityEngine;
using Zenject;

namespace GameModule.StateMachineModule
{
	public abstract class NovelState : IState
	{
		public Action<NovelGameState> onNextState;
		protected NovelState()
		{
			
		}
		
		public virtual void Enter()
		{
			Debug.Log($"{nameof(Enter)}: {GetType().Name}");
		}
		public virtual void Exit()
		{
			
		}
		
	}
}
