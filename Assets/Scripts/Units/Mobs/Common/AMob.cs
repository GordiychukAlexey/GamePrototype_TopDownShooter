using Misc.Damageable;
using Units.Common;
using Units.Common.Weapons.ConcreteWeapons.MobWeapons;
using Units.Mobs.Common.DamageProvisionControl;
using Units.Mobs.Common.MobStates;
using Units.Player;
using UnityEngine;
using Zenject;

namespace Units.Mobs.Common {
	public abstract class AMob : AUnit, IMob {
		private const bool useLog = false;

		[Inject] private IMobModel model;
		[Inject] private MobStateManager stateManager;
		[Inject] private IMobWeapon weapon;

		[Inject] private MobDamageProvider mobDamageProvider;

		public IMobModel Model => model;

		protected void Start(){
			weapon.Owner = this;
		}

		protected override void OnEnable(){
			base.OnEnable();

			collisionSorter.OnCollisionWithDamageable += CollisionWithDamageable;
		}

		protected override void OnDisable(){
			collisionSorter.OnCollisionWithDamageable -= CollisionWithDamageable;

			base.OnDisable();
		}

		private void CollisionWithDamageable(IDamageable damageable){
			if (damageable.DamageableRoot is IPlayer){
				mobDamageProvider.TransmitCollisionDamage(damageable);
			}
		}

		public override void Stop(){
			if (useLog) Debug.Log($"{Name} {nameof(AMob)} {nameof(Stop)}");

			base.Stop();

			stateManager.ChangeState(MobStates.MobStates.Idle);
		}

		public override void Attack(IUnit targetUnit){
			if (useLog) Debug.Log($"{Name} {nameof(AMob)} {nameof(Attack)} {targetUnit.Transform.name}");

			base.Attack(targetUnit);

			stateManager.ChangeState(MobStates.MobStates.Attack);
		}

		public override void Follow(IUnit targetUnit, bool forAttack = false){
			if (useLog){
				Debug.Log(
					$"{Name} {nameof(AMob)} {nameof(Follow)} {targetUnit.Transform.name}, {nameof(forAttack)}: {forAttack}");
			}
			
			base.Follow(targetUnit, forAttack);

			stateManager.ChangeState(MobStates.MobStates.Follow);
		}
	}
}