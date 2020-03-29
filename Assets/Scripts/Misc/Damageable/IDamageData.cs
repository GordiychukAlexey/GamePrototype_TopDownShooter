namespace Misc.Damageable {
	public interface IDamageData {
		float Damage{ get; }
		object DamageSource{ get; }
		object DamageOwner{ get; }
	}
}