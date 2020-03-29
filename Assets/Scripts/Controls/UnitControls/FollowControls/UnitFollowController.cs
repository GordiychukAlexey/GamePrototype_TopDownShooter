using Units.Common;
using UnityEngine;
using Zenject;

namespace Controls.UnitControls.FollowControls {
	public class UnitFollowController : FollowController, IUnitFollowController {
		private readonly UnitFollowModel model;

		[Inject]
		public UnitFollowController(UnitFollowModel model) : base(model){
			this.model = model;
		}

		public override Transform FollowTargetTransform{
			get => FollowTargetUnit != null ? FollowTargetUnit.BodyCenterLocator : base.FollowTargetTransform;
			set{
				base.FollowTargetTransform = value;
				model.FollowTargetUnit     = null;
			}
		}

		public virtual IUnit FollowTargetUnit{
			get => model.FollowTargetUnit;
			set{
				base.FollowTargetTransform = null;
				model.FollowTargetUnit     = value;
			}
		}
	}
}