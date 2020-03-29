using System;
using Main.BaseController;
using UnityEngine;
using Zenject;

namespace Controls.UnitControls.AttackControls {
	public class AttackController : AController, IAttackController {
		public event Action OnMakeShot;

		private readonly AttackModel model;

		[Inject]
		public AttackController(AttackModel model){
			this.model = model;
		}

		public virtual Vector3? AttackTargetPoint{
			get => AttackTargetTransform != null ? AttackTargetTransform.position : model.AttackTargetPoint;
			set{
				model.AttackTargetPoint     = value;
				model.AttackTargetTransform = null;
			}
		}

		public virtual Transform AttackTargetTransform{
			get => model.AttackTargetTransform;
			set{
				model.AttackTargetPoint     = null;
				model.AttackTargetTransform = value;
			}
		}

		protected override void Update(){
			if (AttackTargetPoint.HasValue){
				if (Time.time - model.LastShootTime >= 1.0f / model.AttackSpeed){ //todo fix 1/AttackSpeed
					model.LastShootTime = Time.time;
					OnMakeShot?.Invoke();
				}
			}
		}
	}
}