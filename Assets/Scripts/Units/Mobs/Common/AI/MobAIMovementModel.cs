using System;
using UnityEngine;

namespace Units.Mobs.Common.AI {
	[Serializable]
	public class MobAIMovementModel {
		[SerializeField] private float idleStageTime;
		[SerializeField] private float moveStageTime;

		public float IdleStageTime{
			get => idleStageTime;
			set => idleStageTime = value;
		}

		public float MoveStageTime{
			get => moveStageTime;
			set => moveStageTime = value;
		}

		public MovementStage CurrentMovementStage{ get; set; }

		public float NextStateChangingTime{ get; set; }

		public enum MovementStage {
			Move,
			Idle,
		}
	}
}