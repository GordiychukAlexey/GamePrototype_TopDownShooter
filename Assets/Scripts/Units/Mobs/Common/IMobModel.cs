using Units.Common;
using Units.Mobs.Common.AI;
using Units.Mobs.Common.DamageProvisionControl;

namespace Units.Mobs.Common {
	public interface IMobModel : IUnitModel {
		MobAIMovementModel AiMovementModel{ get; }
		MobDamageProvisionModel DamageProvisionModel{ get; }
	}
}