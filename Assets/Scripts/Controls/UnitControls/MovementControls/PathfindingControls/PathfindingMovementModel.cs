using System;
using UnityEngine;

namespace Controls.UnitControls.MovementControls.PathfindingControls {
	[Serializable]
	public class PathfindingMovementModel : MovementSettings {
		public Vector3? DestinationPoint{ get; set; }
	}
}