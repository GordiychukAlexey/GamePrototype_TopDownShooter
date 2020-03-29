using System;
using System.Collections.Generic;
using Zenject;

namespace Units.Common.StateManager {
	public class StateManager<TState, TStates> : ITickable, IFixedTickable where TState : IState where TStates : Enum {
		public event Action<TStates> OnExitFromState;
		public event Action<TStates> OnEnterToState;

		public TStates CurrentState{ get; private set; }

		private TState currentStateHandler;

		private readonly Dictionary<TStates, TState> states;

		public StateManager(Dictionary<TStates, TState> states){
			this.states = states;

			{
				currentStateHandler = states[default];
				CurrentState        = default;

				currentStateHandler.EnterState();

				OnEnterToState?.Invoke(default);
			}
		}

		public void ChangeState(TStates state){
			if (Equals(state, CurrentState)){
				return;
			}

			TStates previousState = CurrentState;

			currentStateHandler?.ExitState();
			currentStateHandler = states[state];
			CurrentState        = state;
			currentStateHandler.EnterState();

			OnExitFromState?.Invoke(previousState);
			OnEnterToState?.Invoke(default);
		}

		public void Tick(){
			currentStateHandler?.Update();
		}

		public void FixedTick(){
			currentStateHandler?.FixedUpdate();
		}
	}
}