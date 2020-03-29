using UnityEngine;

namespace Units.Common.Weapons {
	public abstract class AWeaponModel : MonoBehaviour { //todo abstract?
		[SerializeField] private Transform projectilesSpawnTransform;
		[SerializeField] private float shootingForce;

		public Transform ProjectilesSpawnTransform => projectilesSpawnTransform;

		public Vector3? AimVector{ get; set; }

		public float ShootingForce{
			get => shootingForce;
			set => shootingForce = value;
		}
	}
}