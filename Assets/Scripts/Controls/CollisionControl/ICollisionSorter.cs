using System;
using Misc.Damageable;
using UnityEngine;

namespace Controls.CollisionControl {
	public interface ICollisionSorter {
		event Action<IDamageable> OnCollisionWithDamageable;
		event Action<IDamaging> OnCollisionWithDamaging;

		void OnCollisionEnter(Collision collision);
		void OnTriggerEnter(Collider other);
	}
}