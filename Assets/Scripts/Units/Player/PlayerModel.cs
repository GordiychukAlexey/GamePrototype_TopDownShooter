using System;
using Units.Common;
using UnityEngine;
using UnityEngine.Animations;

namespace Units.Player {
	[Serializable]
	public class PlayerModel : AUnitModel, IPlayerModel {
		[SerializeField] private Transform weaponPivot;
		[SerializeField] private AimConstraint weaponAimConstraint;
		[SerializeField] private AimController.Model aimModel;
		
		public int CoinsCount{ get; set; }
		public Transform WeaponPivot => weaponPivot;
		public AimConstraint WeaponAimConstraint => weaponAimConstraint;
		public AimController.Model AimModel => aimModel;
	}
}