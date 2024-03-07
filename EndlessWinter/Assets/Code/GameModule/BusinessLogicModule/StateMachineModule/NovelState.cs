using System;
using SharedModule.CustomizeModule;
using SharedModule.StateMachineModule;

namespace GameModule.BusinessLogicModule.StateMachineModule
{
	public abstract class NovelState : IState
	{
		public Action<NovelGameState> onNextState;
		protected NovelState()
		{
			
		}
		
		public virtual void Enter()
		{
			CustomDebug.WriteLine("NovelStateMachine", $"{nameof(Enter)}: {GetType().Name}", CustomDebugColors.Magenta);
		}
		public virtual void Exit()
		{
			
		}
		
	}
}
