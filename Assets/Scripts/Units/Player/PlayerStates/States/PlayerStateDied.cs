using UnityEngine;
using Zenject;

namespace Units.Player.PlayerStates.States {
	public class PlayerStateDied : IPlayerState {
		private const bool useLog = false;

		[Inject] private readonly IPlayer player;

		public void EnterState(){
			if (useLog) Debug.Log($"{player.Name} EnterState Died");
		}

		public void ExitState(){
			if (useLog) Debug.Log($"{player.Name} ExitState Died, braaains!");
		}

		public void Update(){ }

		public void FixedUpdate(){ }
	}
}