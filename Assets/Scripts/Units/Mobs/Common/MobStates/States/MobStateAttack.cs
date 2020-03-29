using Controls.UnitControls.AttackControls;
using Misc.KillerInfo;
using Units.Common;
using Units.Common.Weapons.ConcreteWeapons.MobWeapons;
using UnityEngine;
using Zenject;

namespace Units.Mobs.Common.MobStates.States {
	public class MobStateAttack : IMobState {
		private const bool useLog = false;

		[Inject] private readonly IMob mob;
		[Inject] private readonly IMobWeapon weapon;

		[Inject] private readonly IUnitAttackController attackController;

		private IUnit attackTarget;

		public void EnterState(){
			if (useLog) Debug.Log($"{mob.Name} ExitState Attack");

			attackController.OnMakeShot += Fire;
			attackController.IsActive   =  true;

			attackController.AttackTargetUnit.OnDeath += AttackTargetDeath;


			attackTarget = attackController.AttackTargetUnit;
		}

		public void ExitState(){
			if (useLog) Debug.Log($"{mob.Name} ExitState Attack");

			attackController.IsActive   =  false;
			attackController.OnMakeShot -= Fire;
		}

		private void AttackTargetDeath(IKillerInfo killerInfo){
			if (useLog) Debug.Log($"{mob.Name} enemy {attackController.AttackTargetUnit.Name} is dead ");
			mob.Stop();
		}

		public void Update(){
			if (Vector3.Distance(mob.Transform.position, attackController.AttackTargetUnit.Transform.position) >
				mob.Model.AttackModel.MaxAttackDistance){ //||()//todo проверка прямой видимости
				if (useLog) Debug.Log($"{mob.Name} enemy {attackController.AttackTargetUnit.Name} too far ");
				mob.Follow(attackTarget, true);
				return;
			}

			mob.FaceTarget(attackController.AttackTargetUnit.Transform.position);
		}

		public void FixedUpdate(){ }

		private void Fire(){
			if (useLog) Debug.Log($"{mob.Name} Fire to {attackTarget.Name}");

			weapon.AimVector = attackController.AttackTargetPoint.HasValue
				? attackController.AttackTargetPoint.Value - mob.Model.BodyCenterLocator.position
				: (Vector3?) null;

			weapon.Fire();
		}
	}
}