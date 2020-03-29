using UnityEngine;

namespace Units.Common.Weapons.Projectiles {
	public interface IProjectile {
		float Damage{ get; set; }
		IUnit DamageOwner{ get; }
		float Lifetime{ get; set; }

		void SetPosition(Vector3 position);
		void AppendForce(Vector3 force);
		void Destroy();
	}
}