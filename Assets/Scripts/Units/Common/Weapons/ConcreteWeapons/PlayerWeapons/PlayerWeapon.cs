using Units.Common.Weapons.Projectiles;
using Units.Common.Weapons.Projectiles.ConcreteProjectiles;
using Zenject;

namespace Units.Common.Weapons.ConcreteWeapons.PlayerWeapons {
	public class PlayerWeapon : AUnitWeapon, IPlayerWeapon {
		[Inject] private PlayerWeaponModel model;
		[Inject] private PlayerWeaponBullet.Factory bulletFactory;

		protected override UnitWeaponModel UnitWeaponModel => model;

		protected override IProjectile CreateBullet() => bulletFactory.Create(model.Owner);
	}
}