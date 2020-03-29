using Units.Common;
using UnityEngine;
using Zenject;

namespace Controls.UnitControls.AttackControls {
	public class UnitAttackController : AttackController, IUnitAttackController {
		private readonly UnitAttackModel model;

		[Inject]
		public UnitAttackController(UnitAttackModel model, IUnit unit) : base(model){
			this.model              = model;
			this.model.AttackerUnit = unit;
		}

		public override Vector3? AttackTargetPoint{
			get => AttackTargetUnit?.BodyCenterPosition ?? (AttackTargetTransform != null ? AttackTargetTransform.position : model.AttackTargetPoint);
			set{
				base.AttackTargetPoint = value;
				model.AttackTargetUnit = null;
			}
		}

		public override Transform AttackTargetTransform{
			get => AttackTargetUnit != null ? AttackTargetUnit.Transform : base.AttackTargetTransform;
			set{
				base.AttackTargetTransform = value;
				model.AttackTargetUnit     = null;
			}
		}

		public virtual IUnit AttackTargetUnit{
			get => model.AttackTargetUnit;
			set{
				base.AttackTargetTransform = null;
				model.AttackTargetUnit     = value;
			}
		}

		protected override void Update(){
			if (AttackTargetPoint.HasValue){
				if (Vector3.Distance(model.AttackerUnit.Transform.position, AttackTargetPoint.Value) <= model.MaxAttackDistance){
					//todo проверка прямой видимости
					base.Update();
				}
			}
		}
	}
}