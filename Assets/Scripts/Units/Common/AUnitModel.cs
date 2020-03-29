using Controls.UnitControls.AttackControls;
using Controls.UnitControls.FollowControls;
using Controls.UnitControls.MovementControls;
using Units.Common.HealthControl;
using Units.Common.HUD;
using UnityEngine;

namespace Units.Common {
	public abstract class AUnitModel : MonoBehaviour, IUnitModel {
//		[SerializeField] private float health;
		[SerializeField] private HealthModel healthModel;
		[SerializeField] private UnitMovementModel movementModel;
		[SerializeField] private UnitFollowModel followModel;
		[SerializeField] private UnitAttackModel attackModel;
		[SerializeField] private UnitHudModel hudModel;

		[SerializeField] private Transform bodyCenterLocator;
		
		public HealthModel HealthModel => healthModel;
		public UnitMovementModel MovementModel => movementModel;
		public UnitAttackModel AttackModel => attackModel;
		public UnitFollowModel FollowModel => followModel;
		public UnitHudModel HudModel => hudModel;

		public Transform BodyCenterLocator => bodyCenterLocator;
	}
}