using Units.Mobs.Common;
using UnityEngine;
using Zenject;

namespace Units.Player.PlayerStates.States {
	public class PlayerStateIdle : IPlayerState {
		private const bool useLog = false;

		[Inject] private readonly IPlayer player;
		[Inject] private readonly TargetRadarController targetRadarController;
		[Inject] private readonly AimController aimController;

		public void EnterState(){
			if (useLog) Debug.Log($"{player.Name} EnterState Idle");

			targetRadarController.ClosestMobUpdated += ClosestMobUpdated;

			targetRadarController.IsActive = true;
			aimController.IsActive         = true;
		}

		public void ExitState(){
			if (useLog) Debug.Log($"{player.Name} ExitState Idle");

			aimController.IsActive         = false;
			targetRadarController.IsActive = false;

			targetRadarController.ClosestMobUpdated -= ClosestMobUpdated;
		}

		private void ClosestMobUpdated(IMob closestMob){
			if (closestMob != null){
				if (Vector3.Distance(player.Transform.position, closestMob.Transform.position) < player.Model.AttackModel.MaxAttackDistance){
					if (useLog) Debug.Log($"{player.Name} Enemy {closestMob.Name} in attack area");
					player.Attack(closestMob);
					return;
				}

				player.FaceTarget(closestMob.Transform.position);
			}
		}

		public void Update(){ }

		public void FixedUpdate(){ }
	}
}