using UnityEngine;

namespace Units.Common.Weapons {
	public interface IWeapon {
		Vector3? AimVector{ get; set; }
		float ShootingForce{ get; set; }
		void Fire();
	}
}