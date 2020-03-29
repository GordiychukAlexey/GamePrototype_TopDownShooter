using System;
using Misc.Damageable;
using UnityEngine;

namespace Controls.CollisionControl {
	public class CollisionSorter : ICollisionSorter {
		private const bool useLog = false;

		public event Action<IDamageable> OnCollisionWithDamageable;
		public event Action<IDamaging> OnCollisionWithDamaging;

		public virtual void OnCollisionEnter(Collision collision){
			if (useLog) Debug.Log($"{nameof(CollisionSorter)} {nameof(OnCollisionEnter)} {collision.gameObject.name}");

			Collision(collision.gameObject);
		}

		public virtual void OnTriggerEnter(Collider other){
			if (useLog) Debug.Log($"{nameof(CollisionSorter)} {nameof(OnTriggerEnter)} {other.gameObject.name}");

			Collision(other.gameObject);
		}

		protected virtual void Collision(GameObject other){
			IDamageable damageable = other.GetComponent<IDamageable>();
			if (damageable != null){
				OnCollisionWithDamageable?.Invoke(damageable);
			}

			IDamaging damaging = other.GetComponent<IDamaging>();
			if (damaging != null){
				OnCollisionWithDamaging?.Invoke(damaging);
			}
		}
	}
}