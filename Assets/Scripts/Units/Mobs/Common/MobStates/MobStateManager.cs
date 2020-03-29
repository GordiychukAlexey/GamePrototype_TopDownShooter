using System.Collections.Generic;
using Units.Common.StateManager;
using Units.Mobs.Common.MobStates.States;

namespace Units.Mobs.Common.MobStates {
	public class MobStateManager : StateManager<IMobState, MobStates> {
		public MobStateManager(MobStateIdle idle, MobStateFollow follow, MobStateAttack attack) : base(
			new Dictionary<MobStates, IMobState>(){
				{MobStates.Idle, idle},
				{MobStates.Follow, follow},
				{MobStates.Attack, attack},
			}
		){ }
	}
}