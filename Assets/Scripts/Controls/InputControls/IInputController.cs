using System;
using UnityEngine;

namespace Controls.InputControls {
	public interface IInputController {
		event Action<Vector2> InputValueSet;
		Vector2 InputValue{ get; }
	}
}