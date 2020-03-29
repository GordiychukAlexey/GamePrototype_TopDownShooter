using System;
using Misc.Damageable;
using UnityEngine;

namespace Units.Mobs.Common.DamageProvisionControl {
	public interface IMobDamageProvisionModel : IDamageProvisionModel {
		float CollisionDamage{ get; set; }
	}

	[Serializable]
	public class MobDamageProvisionModel : IMobDamageProvisionModel {
		[SerializeField] private float collisionDamage;

		public float CollisionDamage{
			get => collisionDamage;
			set => collisionDamage = value;
		}
	}
}