using System.Collections.Generic;
using Units.Common.StateManager;
using Units.Player.PlayerStates.States;

namespace Units.Player.PlayerStates {
	public class PlayerStateManager : StateManager<IPlayerState, PlayerStates> {
		public PlayerStateManager(PlayerStateIdle idle, PlayerStateMove move, PlayerStateAttack attack, PlayerStateDied died) : base(
			new Dictionary<PlayerStates, IPlayerState>(){
				{PlayerStates.Idle, idle},
				{PlayerStates.Move, move},
				{PlayerStates.Attack, attack},
				{PlayerStates.Died, died},
			}
		){ }
	}
}