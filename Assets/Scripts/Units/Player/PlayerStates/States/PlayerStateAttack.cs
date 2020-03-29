using Controls.UnitControls.AttackControls;
using Units.Common.Weapons.ConcreteWeapons.PlayerWeapons;
using Units.Mobs.Common;
using UnityEngine;
using Zenject;

namespace Units.Player.PlayerStates.States {
	public class PlayerStateAttack : IPlayerState {
		private const bool useLog = false;

		[Inject] private readonly IPlayer player;
		[Inject] private readonly IPlayerWeapon weapon;
		[Inject] private readonly TargetRadarController targetRadarController;
		[Inject] private readonly IUnitAttackController attackController;
		[Inject] private readonly AimController aimController;

		public void EnterState(){
			if (useLog) Debug.Log($"{player.Name} EnterState Attack");

			targetRadarController.ClosestMobUpdated += ClosestMobUpdated;
			targetRadarController.IsActive          =  true;

			aimController.OnAimed     += OnAimed;
			aimController.OnMissAimed += OnMissAimed;
			aimController.IsActive    =  true;

			attackController.IsActive   =  false; //false! так как стрельба идёт после наведения на цель
			attackController.OnMakeShot += weapon.Fire;
		}

		public void ExitState(){
			if (useLog) Debug.Log($"{player.Name} ExitState Attack");

			targetRadarController.IsActive          =  false;
			targetRadarController.ClosestMobUpdated -= ClosestMobUpdated;

			aimController.IsActive    =  false;
			aimController.OnAimed     -= OnAimed;
			aimController.OnMissAimed -= OnMissAimed;

			attackController.IsActive   =  false;
			attackController.OnMakeShot -= weapon.Fire;
		}

		private void OnAimed() => attackController.IsActive = true;

		private void OnMissAimed() => attackController.IsActive = false;

		private void ClosestMobUpdated(IMob closestMob){
			if (closestMob == null){
				if (useLog) Debug.Log($"{player.Name} Mobs not found ");
				player.Stop();
				return;
			}

			if (Vector3.Distance(player.Transform.position, closestMob.Transform.position)
				> player.Model.AttackModel.MaxAttackDistance){
				if (useLog) Debug.Log($"{player.Name} All mobs too far ");
				player.Stop();
				return;
			}

			attackController.AttackTargetUnit = closestMob;

			player.FaceTarget(closestMob.Transform.position);
		}

		public void Update(){ }

		public void FixedUpdate(){ }
	}
}