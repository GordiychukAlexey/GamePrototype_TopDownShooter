using Units.Common;
using UnityEngine;
using UnityEngine.Animations;

namespace Units.Player {
	public interface IPlayerModel : IUnitModel {
		int CoinsCount{ get; set; }
		Transform WeaponPivot{ get; }
		AimController.Model AimModel{ get; }
	}
}