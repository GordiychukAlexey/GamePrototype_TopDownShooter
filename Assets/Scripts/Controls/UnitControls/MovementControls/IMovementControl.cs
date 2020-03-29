using UnityEngine;

namespace Controls.UnitControls.MovementControls {
	public interface IMovementControl {
		Vector3 MoveValue{ get; set; }
		bool IsStopped{ get; set; }
	}
}