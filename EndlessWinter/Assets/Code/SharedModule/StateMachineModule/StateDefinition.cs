using System;

namespace SharedModule.StateMachineModule
{
	public interface IStateDefinition
	{
		Type Type { get; }
		IState GetState();
	}
	
	class StateDefinition<T> : IStateDefinition where T : IState
	{
		private readonly StateFactory<T> _factory;
		private readonly Type _type;

		public Type Type => _type;


		public StateDefinition(StateFactory<T> __factory)
		{
			this._factory = __factory;
			
			this._type = typeof(T);
		}


		public IState GetState() => _factory.Invoke();
	}
}
