namespace Misc.Damageable {
	/// <summary>
	/// Повреждаемый
	/// </summary>
	public interface IDamageable {
		object DamageableRoot{ get; }
		void ReceiveDamage(IDamageData damageData);
	}
}