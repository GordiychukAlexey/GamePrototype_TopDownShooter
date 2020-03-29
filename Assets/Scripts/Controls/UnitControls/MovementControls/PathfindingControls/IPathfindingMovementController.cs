using System;
using Main.BaseController;
using UnityEngine;

namespace Controls.UnitControls.MovementControls.PathfindingControls {
	public interface IPathfindingMovementController : IMovementControl, IController {
		event Action OnDestinationPointReached;
		Vector3? DestinationPoint{ get; set; }
	}
}