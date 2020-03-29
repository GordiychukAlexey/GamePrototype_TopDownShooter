using Units.Common;

namespace Controls.UnitControls.FollowControls {
	public interface IUnitFollowController : IFollowController {
		IUnit FollowTargetUnit{ get; set; }
	}
}