using System;
using UnityEngine;

namespace Controls.UnitControls.AttackControls {
	[Serializable]
	public class AttackModel {
		[SerializeField] private float attackSpeed;
//		[SerializeField] private float attackDamage;

		public float AttackSpeed{
			get => attackSpeed;
			set => attackSpeed = value;
		}

//		//todo назначать пуле? перенести в WeaponModel/WeaponBulletModel?
//		public float AttackDamage{
//			get => attackDamage;
//			set => attackDamage = value;
//		}

		public Vector3? AttackTargetPoint{ get; set; }
		public Transform AttackTargetTransform{ get; set; }

		public bool IsShooting{ get; set; }
		public float LastShootTime{ get; set; }
	}
}