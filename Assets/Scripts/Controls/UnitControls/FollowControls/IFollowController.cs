using System;
using Main.BaseController;
using UnityEngine;

namespace Controls.UnitControls.FollowControls {
	public interface IFollowController : IController {
		event Action<Vector3> OnDestinationPointUpdated;
		event Action FollowTargetDisappeared;

		Transform FollowTargetTransform{ get; set; }
	}
}