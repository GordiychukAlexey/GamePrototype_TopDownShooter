using Units.Common.Weapons.Projectiles;
using UnityEngine;

namespace Units.Common.Weapons {
	public abstract class AWeapon : MonoBehaviour, IWeapon {
		private const bool useLog = false;

		protected abstract AWeaponModel WeaponModel{ get; }

		protected abstract IProjectile CreateBullet();

		public Vector3? AimVector{
			get => WeaponModel.AimVector;
			set => WeaponModel.AimVector = value?.normalized;
		}

		public float ShootingForce{
			get => WeaponModel.ShootingForce;
			set => WeaponModel.ShootingForce = value;
		}

		protected virtual Vector3 ShootingVector => WeaponModel.AimVector ?? WeaponModel.ProjectilesSpawnTransform.forward;

		public void Fire(){
			if (useLog) Debug.Log($"AWeapon Fire");

			IProjectile newBullet = CreateBullet();
			newBullet.SetPosition(WeaponModel.ProjectilesSpawnTransform.position);
			newBullet.AppendForce(ShootingVector * WeaponModel.ShootingForce);
		}
	}
}