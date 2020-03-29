using System;
using Main.BaseController;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Units.Mobs.Common.AI {
	public class MobAIMovementController : AController, IMobAIMovementController {
		[Inject] private readonly MobAIMovementModel model;

		public MobAIMovementModel.MovementStage CurrentMovementStage{
			get => model.CurrentMovementStage; //todo вернуть
			private set => model.CurrentMovementStage = value;
		}

		private void SetStage(MobAIMovementModel.MovementStage stage){
			if (stage != CurrentMovementStage){
				CurrentMovementStage = stage;

				switch (CurrentMovementStage){
					case MobAIMovementModel.MovementStage.Move:
						model.NextStateChangingTime = Time.time + model.MoveStageTime * (1.0f - Random.value * 0.5f);
						break;
					case MobAIMovementModel.MovementStage.Idle:
						model.NextStateChangingTime = Time.time + model.IdleStageTime * (1.0f - Random.value * 0.5f);
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}
			}
		}

		protected override void Awake(){
//			SetStage(MobAIMovementModel.MovementStage.Idle);//todo вернуть
			SetStage(MobAIMovementModel.MovementStage.Move);
		}

		protected override void Update(){
			if (Time.time > model.NextStateChangingTime){
				switch (CurrentMovementStage){
					case MobAIMovementModel.MovementStage.Move:
						SetStage(MobAIMovementModel.MovementStage.Idle);
						break;
					case MobAIMovementModel.MovementStage.Idle:
						SetStage(MobAIMovementModel.MovementStage.Move);
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}
			}
		}
	}
}