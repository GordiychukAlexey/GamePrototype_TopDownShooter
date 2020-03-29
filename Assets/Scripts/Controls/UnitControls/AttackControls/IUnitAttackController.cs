using Units.Common;

namespace Controls.UnitControls.AttackControls {
	public interface IUnitAttackController : IAttackController {
		IUnit AttackTargetUnit{ get; set; }
	}
}