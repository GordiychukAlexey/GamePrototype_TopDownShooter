namespace Misc.Damageable {
	public class DamageData : IDamageData {
		public float Damage{ get; }
		public object DamageSource{ get; }
		public object DamageOwner{ get; }

		public DamageData(float damage, object damageSource, object damageOwner){
			Damage       = damage;
			DamageSource = damageSource;
			DamageOwner  = damageOwner;
		}

		public override string ToString() =>
			$"{nameof(Damage)}: {Damage} {nameof(DamageSource)}: {DamageSource?.GetType().Name ?? "null"} {nameof(DamageOwner)}: {DamageOwner?.GetType().Name ?? "null"}";
	}
}