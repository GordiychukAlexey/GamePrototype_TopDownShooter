using Controls.UnitControls.AttackControls;
using Units.Common;
using UnityEngine;
using Zenject;

namespace Tools {
	public class AimHelperForUnitWeapon : MonoBehaviour {
		[Inject] private IUnit unit;
		[Inject] private UnitAttackModel model;

		[Range(0.0f, 180.0f)] public float StartTargetingAngleToTarget = 30.0f;
		[Range(0.0f, 500.0f)] public float TargetingSpeed = 200.0f;

		private void Update(){
			Vector3 targetDir = unit.Transform.forward;
			if (model.AttackTargetUnit != null){
				Vector3 targetDirProject = Vector3.ProjectOnPlane(model.AttackTargetUnit.BodyCenterPosition - transform.position, Vector3.up);
				Vector3 forwardProject   = Vector3.ProjectOnPlane(unit.Transform.forward, Vector3.up);
				float   angleToTarget    = Vector3.Angle(forwardProject, targetDirProject);

				if (angleToTarget <= StartTargetingAngleToTarget){
					targetDir = model.AttackTargetUnit.BodyCenterPosition - transform.position;
				}
			}

			Quaternion rotation = Quaternion.LookRotation(targetDir);
			transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, TargetingSpeed * Time.deltaTime);
		}
	}
}