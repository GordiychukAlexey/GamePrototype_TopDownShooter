using Misc.Damageable;
using Zenject;

namespace Units.Mobs.Common.DamageProvisionControl {
	public class MobDamageProvider : IMobDamageProvider {
		[Inject] private IMob mob;
		[Inject] private readonly IMobDamageProvisionModel mobDamageProvisionModel;

		public void TransmitCollisionDamage(IDamageable damageable){
			damageable?.ReceiveDamage(new DamageData(mobDamageProvisionModel.CollisionDamage, mob, mob));
		}
	}
}