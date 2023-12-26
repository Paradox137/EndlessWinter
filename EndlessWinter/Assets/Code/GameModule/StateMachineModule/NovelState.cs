using SharedModule.StateMachineModule;
using UnityEngine;

namespace GameModule.StateMachineModule
{
	public abstract class NovelState : IState
	{
		private NovelStateMachine _machine;

		protected NovelState(NovelStateMachine __machine)
		{
			_machine = __machine;
		}
		
		public virtual void Enter()
		{
			Debug.Log($"{nameof(Enter)}: {GetType().Name}");
		}
		public virtual void Exit()
		{
			
		}
		
		protected void NextState(NovelGameState state) => _machine.Fire(state);
	}
}
