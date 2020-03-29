using System;
using Main.BaseController;
using UnityEngine;

namespace Controls.UnitControls.AttackControls {
	public interface IAttackController : IController {
		event Action OnMakeShot;

		Vector3? AttackTargetPoint{ get; set; }

		Transform AttackTargetTransform{ get; set; }
	}
}