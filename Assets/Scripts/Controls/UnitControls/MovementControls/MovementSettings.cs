using System;
using UnityEngine;

namespace Controls.UnitControls.MovementControls {
	[Serializable]
	public class MovementSettings {
		[SerializeField] private float moveSpeed;

		public float MoveSpeed{
			get => moveSpeed;
			set => moveSpeed = value;
		}

		public Vector3 MoveValue{ get; set; }
	}
}