using UnityEngine;
using Zenject;

namespace Misc.Damageable {
	[RequireComponent(typeof(Collider))]
	public class DamageableCollisionMediator : MonoBehaviour, IDamageable {
		[Inject] private readonly object damageableRoot;
		[Inject] private readonly IDamageable damageable;
		public object DamageableRoot => damageableRoot;
		public void ReceiveDamage(IDamageData damageData) => damageable.ReceiveDamage(damageData);
	}
}