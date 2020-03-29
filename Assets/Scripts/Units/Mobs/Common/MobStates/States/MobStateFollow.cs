using System;
using Controls.UnitControls.AttackControls;
using Controls.UnitControls.FollowControls;
using Controls.UnitControls.MovementControls.PathfindingControls;
using Units.Mobs.Common.AI;
using UnityEngine;
using Zenject;

namespace Units.Mobs.Common.MobStates.States {
	public class MobStateFollow : IMobState {
		private const bool useLog = false;

		[Inject] private readonly IMob mob;
		[Inject] private readonly IUnitFollowController followController;
		[Inject] private readonly IPathfindingMovementController movementController;
		[Inject] private readonly IUnitAttackController attackController;
		[Inject] private readonly IMobAIMovementController aiMovementController;

		public void EnterState(){
			if (useLog) Debug.Log($"{mob.Name} EnterState Follow");

			followController.OnDestinationPointUpdated += DestinationPointUpdated;
			followController.FollowTargetDisappeared   += FollowTargetDisappeared;
		}

		public void ExitState(){
			if (useLog) Debug.Log($"{mob.Name} ExitState Follow");

			followController.OnDestinationPointUpdated -= DestinationPointUpdated;
			followController.FollowTargetDisappeared   -= FollowTargetDisappeared;
		}

		private void DestinationPointUpdated(Vector3 position){
			if (Vector3.Distance(mob.Transform.position, position) <= mob.Model.AttackModel.MaxAttackDistance){
				//todo проверка прямой видимости
				if (followController.FollowTargetUnit == attackController.AttackTargetUnit){
					if (useLog) Debug.Log($"{mob.Name} Enemy {attackController.AttackTargetUnit.Name} in attack area");

					mob.Attack(attackController.AttackTargetUnit);
					return;
				}
			}
		}

		private void FollowTargetDisappeared(){
//			mob.Stop();
		}

		public void Update(){
			switch (aiMovementController.CurrentMovementStage){
				case MobAIMovementModel.MovementStage.Move:
					movementController.IsStopped = false;
					break;
				case MobAIMovementModel.MovementStage.Idle:
					movementController.IsStopped = true;
					mob.FaceTarget(attackController.AttackTargetUnit.Transform.position, 180.0f);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		public void FixedUpdate(){ }
	}
}