using System;
using Main.BaseController;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Controls.UnitControls.MovementControls.PathfindingControls {
	public class NavMeshPathfindingMovementController : AController, IPathfindingMovementController {
		public event Action OnDestinationPointReached;

		[Inject] private NavMeshAgent navMeshAgent;
		[Inject] private PathfindingMovementModel model;

		protected override void Awake(){
			navMeshAgent.speed = model.MoveSpeed;
		}

		public Vector3 MoveValue{
			get => model.MoveValue;
			set{
				model.MoveValue        = value;
				model.DestinationPoint = null;
			}
		}


		public Vector3? DestinationPoint{
			get => model.DestinationPoint;
			set{
				model.MoveValue        = Vector3.zero;
				model.DestinationPoint = value;
			}
		}

		public bool IsStopped{ get; set; }

		protected override void Update(){
			navMeshAgent.isStopped = IsStopped;

			if (IsStopped){
				return;
			}

			if (model.MoveValue != Vector3.zero){
				navMeshAgent.velocity  = model.MoveValue * navMeshAgent.speed;
				navMeshAgent.isStopped = false;
				return;
			}

			if (DestinationPoint.HasValue){
				navMeshAgent.SetDestination(DestinationPoint.Value);
				navMeshAgent.isStopped = false;
				return;
			}

			navMeshAgent.isStopped = true;
		}
	}
}