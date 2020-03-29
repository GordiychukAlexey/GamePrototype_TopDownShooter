using System;
using Controls.InputControls;
using UnityEngine;
using Zenject;

namespace Main {
	public class InputAdapter_GroundMovement : IInitializable {
		public event Action<Vector3> OnInputValueSet;

		[Inject] private IInputController inputController;

		public void Initialize(){
			inputController.InputValueSet += (Vector2 inputValue) => {
				Vector3 adapted = new Vector3(inputController.InputValue.x, 0.0f, inputController.InputValue.y);
				OnInputValueSet?.Invoke(adapted);
			};
		}
	}
}