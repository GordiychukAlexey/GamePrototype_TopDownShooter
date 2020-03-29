using System;
using Units.Common;
using UnityEngine;

namespace Controls.UnitControls.AttackControls {
	[Serializable]
	public class UnitAttackModel : AttackModel {
		[SerializeField] private float maxAttackDistance;

		public float MaxAttackDistance{
			get => maxAttackDistance;
			set => maxAttackDistance = value;
		}

		public IUnit AttackerUnit{ get; set; }
		public IUnit AttackTargetUnit{ get; set; }
	}
}