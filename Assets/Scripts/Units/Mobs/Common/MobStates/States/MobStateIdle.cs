using UnityEngine;
using Zenject;

namespace Units.Mobs.Common.MobStates.States {
	public class MobStateIdle : IMobState {
		private const bool useLog = false;

		[Inject] private readonly IMob mob;

		public void EnterState(){
			if (useLog) Debug.Log($"{mob.Name} EnterState Idle");
		}

		public void ExitState(){
			if (useLog) Debug.Log($"{mob.Name} ExitState Idle");
		}

		public void Update(){ }

		public void FixedUpdate(){ }
	}
}