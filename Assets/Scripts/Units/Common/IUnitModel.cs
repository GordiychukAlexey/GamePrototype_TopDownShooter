using Controls.UnitControls.AttackControls;
using Controls.UnitControls.FollowControls;
using Controls.UnitControls.MovementControls;
using Units.Common.HealthControl;
using Units.Common.HUD;
using UnityEngine;

namespace Units.Common {
	//todo IUnitModel
	public interface IUnitModel {
		HealthModel HealthModel{ get; }
		UnitMovementModel MovementModel{ get; }
		UnitAttackModel AttackModel{ get; }
		UnitFollowModel FollowModel{ get; }
		UnitHudModel HudModel{ get; }

		Transform BodyCenterLocator{ get; }
	}
}