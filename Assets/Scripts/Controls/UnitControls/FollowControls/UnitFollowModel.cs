using System;
using Units.Common;

namespace Controls.UnitControls.FollowControls {
	[Serializable]
	public class UnitFollowModel : FollowModel {
		private IUnit followTargetUnit;

		public IUnit FollowTargetUnit{
			get => followTargetUnit;
			set{
				followTargetUnit      = value;
				FollowTargetTransform = value?.Transform;
			}
		}
	}
}