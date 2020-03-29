using Zenject;

namespace Units.Common.Weapons.Projectiles.ConcreteProjectiles {
	public class MobWeaponBullet : AProjectile {
		private void Start(){
			ThisRigidbody.useGravity = false;
		}

		public class Factory : PlaceholderFactory<IUnit, MobWeaponBullet> { }
	}
}