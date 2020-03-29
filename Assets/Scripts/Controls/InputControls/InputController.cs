using System;
using UnityEngine;
using Zenject;

namespace Controls.InputControls {
	public class InputController : ITickable, IInputController {
		public Vector2 InputValue{ get; private set; }
		public event Action<Vector2> InputValueSet;

		public void Tick(){
			Vector2 newInput                   = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
			Vector2 newInputNormalized         = newInput.normalized;
			Vector2 newInputNormalizedAbsolute = new Vector2(Mathf.Abs(newInputNormalized.x), Mathf.Abs(newInputNormalized.y));

			InputValue = new Vector2(
				Map(newInput.x, -1.0f, 1.0f, -newInputNormalizedAbsolute.x, newInputNormalizedAbsolute.x),
				Map(newInput.y, -1.0f, 1.0f, -newInputNormalizedAbsolute.y, newInputNormalizedAbsolute.y)
			);

			InputValueSet?.Invoke(InputValue);
		}

		private static float Map(float sourceValue, float fromSource, float toSource, float fromTarget, float toTarget){
			if (fromSource == toSource){
				if (sourceValue < fromSource){
					return Mathf.NegativeInfinity;
				} else if (sourceValue > toSource){
					return Mathf.Infinity;
				} else{
					return fromTarget + (toTarget - fromTarget) / 2.0f;
				}
			} else{
				return (sourceValue - fromSource) / (toSource - fromSource) * (toTarget - fromTarget) + fromTarget;
			}
		}
	}
}