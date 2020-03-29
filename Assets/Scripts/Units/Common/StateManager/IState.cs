namespace Units.Common.StateManager {
	public interface IState {
//		void InitializeState();
		void EnterState();
		void ExitState();
		void Update();
		void FixedUpdate();
	}
}