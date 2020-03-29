using System;

namespace Units.Common.StateManager {
	public class SimpleStateManager<TState> where TState : Enum {
		/// <summary>
		/// previousState/newState
		/// </summary>
		public event Action<TState, TState> OnStateChanged;

		private TState currentState;

		public TState CurrentState{
			get => currentState;
			private set{
				TState lastState = currentState;
				currentState = value;
				OnStateChanged?.Invoke(lastState, currentState);
			}
		}

		private TState currentStateHandler;

		public SimpleStateManager(){
			SetState(default);
		}

		public void ChangeState(TState state){
			if (Equals(state, CurrentState)){
				return;
			}

			SetState(state);
		}

		private void SetState(TState state){
			if (!Equals(state, CurrentState)){
				CurrentState = state;
			}
		}
	}
}