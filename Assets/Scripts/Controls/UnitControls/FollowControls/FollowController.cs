using System;
using Main.BaseController;
using UnityEngine;
using Zenject;

namespace Controls.UnitControls.FollowControls {
	public class FollowController : AController, IFollowController {
		public event Action<Vector3> OnDestinationPointUpdated;
		public event Action FollowTargetDisappeared;
		private readonly FollowModel model;

		[Inject]
		public FollowController(FollowModel model){
			this.model = model;
		}

		public virtual Transform FollowTargetTransform{
			get => model.FollowTargetTransform;
			set => model.FollowTargetTransform = value;
		}

		protected override void Update(){
			if (FollowTargetTransform){
				OnDestinationPointUpdated?.Invoke(FollowTargetTransform.position);
			} else{
				FollowTargetDisappeared?.Invoke();
			}
		}
	}
}