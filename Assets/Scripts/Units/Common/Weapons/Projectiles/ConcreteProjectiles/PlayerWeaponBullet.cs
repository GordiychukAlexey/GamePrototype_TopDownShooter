using Zenject;

namespace Units.Common.Weapons.Projectiles.ConcreteProjectiles {
	public class PlayerWeaponBullet : AProjectile {
		private void Start(){
			ThisRigidbody.useGravity = false;
		}

		public class Factory : PlaceholderFactory<IUnit, PlayerWeaponBullet> { }
	}
}