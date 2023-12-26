using System;
using System.Collections.Generic;
using UnityEngine.Assertions;

namespace SharedModule.StateMachineModule
{
	public delegate T StateFactory<out T>();
	
	public class LogicStateMachine<TTrigger> : IStateMachine<TTrigger>, IStateMachineDefinition<TTrigger>
	{
		private readonly Dictionary<Type, IStateDefinition> _states = new Dictionary<Type, IStateDefinition>();
		private readonly Dictionary<(Type, TTrigger), IStateDefinition> _transitions = new Dictionary<(Type, TTrigger), IStateDefinition>();

		private (IStateDefinition Definition, IState State) _current;

		public void Fire(TTrigger __trigger)
		{
			var transition = (_current.Definition?.Type, trigger: __trigger);

			AssertTransition(transition, __trigger);

			var definition = _transitions[transition];

			_current.State?.Exit();
			_current = (definition, definition?.GetState());
			_current.State?.Enter();

			Assert.IsNotNull(_current.State, $"State cant be null. Trigger Type[{typeof(TTrigger).Name}] Key[{__trigger.ToString()}]");
		}

		public void DefineState<T>(StateFactory<T> __factory) where T : IState
		{
			var type = typeof(T);
			var definition = new StateDefinition<T>(__factory);

			_states.Add(type, definition);
		}

		public void DefineTransition<T1, T2>(TTrigger __trigger) where T1 : IState where T2 : IState
		{
			var type1 = typeof(T1);
			var type2 = typeof(T2);

			AssertState(type1);
			AssertState(type2);

			_transitions.Add((type1, __trigger), _states[type2]);
		}

		public void DefineStartTransition<T>(TTrigger __trigger) where T : IState
		{
			var type = typeof(T);

			AssertState(type);

			_transitions.Add((null, __trigger), _states[type]);
		}

		private void AssertState(Type __type)
		{
			string message = $"State of type {__type.Name} not defined";

			Assert.IsTrue(_states.ContainsKey(__type), message);
		}
		private void AssertTransition((Type, TTrigger) transition, TTrigger trigger)
		{
			string message = $"Transition from state[{_current.State?.GetType().Name ?? "ROOT"}] not found by trigger {trigger}";

			Assert.IsTrue(_transitions.ContainsKey(transition), message);
		}
	}
}
