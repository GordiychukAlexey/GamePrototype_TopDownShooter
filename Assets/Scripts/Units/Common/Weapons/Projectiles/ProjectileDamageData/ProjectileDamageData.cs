using Misc.Damageable;

namespace Units.Common.Weapons.Projectiles.ProjectileDamageData {
	public class ProjectileDamageData : DamageData, IProjectileDamageData {
		public ProjectileDamageData(float damage, object damageSource, object damageOwner) : base(damage, damageSource, damageOwner){ }
	}
}