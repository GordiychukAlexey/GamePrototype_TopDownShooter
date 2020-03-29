using System;
using Controls.UnitControls.AttackControls;
using Main.BaseController;
using UnityEngine;
using Zenject;

namespace Units.Player {
	public class AimController : AController {
		public event Action OnAimed;
		public event Action OnMissAimed;

		[Inject] private readonly IPlayer player;
		[Inject] private readonly Model model;
		[Inject] private readonly UnitAttackModel unitAttackModel;

		private bool currentAimedState = false;

		protected override void Update(){
			Vector3 targetDir = player.Transform.forward;

			bool isAim = false;
			if (unitAttackModel.AttackTargetUnit != null && !unitAttackModel.AttackTargetUnit.IsDead){ //todo rjcnskm
				Vector3 weaponToTargetDirection     = unitAttackModel.AttackTargetUnit.BodyCenterPosition - player.Model.WeaponPivot.position;
				Vector3 weaponPivotForwardDirection = player.Model.WeaponPivot.forward;
				float   weaponToTargetAngle         = Vector3.Angle(weaponPivotForwardDirection, weaponToTargetDirection);

				Vector3 betweenCentersGroundProjectedDirection =
					Vector3.ProjectOnPlane(unitAttackModel.AttackTargetUnit.BodyCenterPosition - player.BodyCenterPosition, Vector3.up);
				Vector3 playerForwardGroundProjectedDirection = Vector3.ProjectOnPlane(player.Transform.forward, Vector3.up);
				float playerForwardToTargetAngle =
					Vector3.Angle(playerForwardGroundProjectedDirection, betweenCentersGroundProjectedDirection);

				if (playerForwardToTargetAngle <= model.StartTargetingAngleToTarget){
					targetDir = unitAttackModel.AttackTargetUnit.BodyCenterPosition - player.Model.WeaponPivot.position;
				}

				isAim = weaponToTargetAngle < 5.0f; //Немного magic numgers
			}

			Quaternion rotation = Quaternion.LookRotation(targetDir);
			player.Model.WeaponPivot.rotation =
				Quaternion.RotateTowards(player.Model.WeaponPivot.rotation, rotation, model.TargetingSpeed * Time.deltaTime);


			if (currentAimedState != isAim){
				currentAimedState = isAim;

				if (currentAimedState){
					OnAimed?.Invoke();
				} else{
					OnMissAimed?.Invoke();
				}
			}
		}

		protected override void FixedUpdate(){ }

		[Serializable]
		public class Model {
			[Range(0.0f, 180.0f)] public float StartTargetingAngleToTarget = 30.0f;
			[Range(0.0f, 500.0f)] public float TargetingSpeed = 200.0f;
		}
	}
}