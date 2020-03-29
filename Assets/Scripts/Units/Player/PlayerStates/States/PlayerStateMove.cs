using UnityEngine;
using Zenject;

namespace Units.Player.PlayerStates.States {
	public class PlayerStateMove : IPlayerState {
		private const bool useLog = false;

		[Inject] private readonly IPlayer player;
		[Inject] private readonly AimController aimController;

		public void EnterState(){
			if (useLog) Debug.Log($"{player.Name} EnterState Move");
			aimController.IsActive = true;
		}

		public void ExitState(){
			if (useLog) Debug.Log($"{player.Name} ExitState Move");

			aimController.IsActive = false;
		}

		public void Update(){ }

		public void FixedUpdate(){ }
	}
}