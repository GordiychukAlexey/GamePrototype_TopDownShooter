using System;
using Units.Common;
using Units.Mobs.Common.AI;
using Units.Mobs.Common.DamageProvisionControl;
using UnityEngine;

namespace Units.Mobs.Common {
	[Serializable]
	public abstract class AMobModel : AUnitModel, IMobModel {
		[SerializeField] private MobAIMovementModel aiMovementModel;
		[SerializeField] private MobDamageProvisionModel damageProvisionModel;

		public MobAIMovementModel AiMovementModel => aiMovementModel;

		public MobDamageProvisionModel DamageProvisionModel => damageProvisionModel;
	}
}