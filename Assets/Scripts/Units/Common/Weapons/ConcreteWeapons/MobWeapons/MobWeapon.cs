using Units.Common.Weapons.Projectiles;
using Units.Common.Weapons.Projectiles.ConcreteProjectiles;
using Zenject;

namespace Units.Common.Weapons.ConcreteWeapons.MobWeapons {
	public class MobWeapon : AUnitWeapon, IMobWeapon {
		[Inject] private MobWeaponModel model;
		[Inject] private MobWeaponBullet.Factory bulletFactory;

		protected override UnitWeaponModel UnitWeaponModel => model;

		protected override IProjectile CreateBullet() => bulletFactory.Create(model.Owner);
	}
}