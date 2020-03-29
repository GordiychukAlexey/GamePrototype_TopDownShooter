using System;
using Controls.CollisionControl;
using Controls.UnitControls.AttackControls;
using Controls.UnitControls.FollowControls;
using Controls.UnitControls.MovementControls.PathfindingControls;
using Misc.Damageable;
using Misc.KillerInfo;
using Units.Common.HealthControl;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Units.Common {
	public abstract class AUnit : MonoBehaviour, IUnit {
		private const bool useLog = false;

		public event Action<float> OnHealthUpdated;

		public event Action<IKillerInfo> OnDeath;

		public event Action OnDestroy;

		[Inject] private readonly IUnitModel model;

		[Inject] protected readonly IHealthController healthController;
		[Inject] protected readonly IPathfindingMovementController movementController;
		[Inject] protected readonly IUnitFollowController followController;
		[Inject] protected readonly IUnitAttackController attackController;

		[Inject] protected readonly ICollisionSorter collisionSorter;

		public string Name => gameObject.name;
		public float MaxHealth => model.HealthModel.MaxHealth;

		public Transform Transform => transform;

		public Transform BodyCenterLocator => model.BodyCenterLocator;
		public Vector3 BodyCenterPosition => model.BodyCenterLocator.position;

		public Vector3 HudLocatorPosition => model.HudModel.HudLocator.position;

		public bool IsDead => model.HealthModel.IsDead;

		public object PhysicalRoot => this;
		public object DamageableRoot => this;


		[ContextMenu(nameof(OnEnable))]
		protected virtual void OnEnable(){
			healthController.OnHealthUpdated += HealthUpdated;
			followController.OnDestinationPointUpdated += DestinationPointUpdated;

			healthController.IsActive   = true;
			movementController.IsActive = true;
			followController.IsActive   = true;
			attackController.IsActive   = true;
		}

		[ContextMenu(nameof(OnDisable))]
		protected virtual void OnDisable(){
			healthController.IsActive   = false;
			movementController.IsActive = false;
			followController.IsActive   = false;
			attackController.IsActive   = false;

			healthController.OnHealthUpdated -= HealthUpdated;
			followController.OnDestinationPointUpdated -= DestinationPointUpdated;
		}

		private void HealthUpdated(float newHealthValue){
			if (useLog) Debug.Log($"{Name} {nameof(HealthUpdated)} {newHealthValue}");

			OnHealthUpdated?.Invoke(newHealthValue);
		}

		private void DestinationPointUpdated(Vector3 destinationPoint) => movementController.DestinationPoint = destinationPoint;


		public virtual void ReceiveDamage(IDamageData damageData){
			if (useLog) Debug.Log($"{Name} {nameof(ReceiveDamage)} {damageData}");

			healthController.ReceiveDamage(damageData.Damage);

			if (healthController.CurrentHealth <= 0.0f){
				Kill(new KillerInfo(damageData.DamageOwner, damageData.DamageSource));
			}
		}

		public virtual void Stop(){
			if (useLog) Debug.Log($"{Name} {nameof(AUnit)} {nameof(Stop)}");

			movementController.DestinationPoint = null;
			followController.FollowTargetUnit   = null;
			attackController.AttackTargetUnit   = null;
		}

		//Так как юнит использует интерфейс IPathfindingMovementController (может быть и без NavMeshAgent-а)
		public void SetPosition(Vector3 position){
			NavMeshAgent navMeshAgent = GetComponent<NavMeshAgent>();
			if (navMeshAgent){
				navMeshAgent.updatePosition = false;
			}

			transform.position = position;
			if (navMeshAgent){
				navMeshAgent.updatePosition = true;
			}
		}

		public virtual void Move(Vector3 moveValue){
			if (moveValue != movementController.MoveValue){
//				if (useLog) Debug.Log($"{Name} {nameof(AUnit)} {nameof(Move)} {nameof(moveValue)}: {moveValue}");

				movementController.MoveValue      = moveValue;
				followController.FollowTargetUnit = null;
				attackController.AttackTargetUnit = null;
			}
		}

		public virtual void SetDestination(Vector3 destination){
			if (useLog) Debug.Log($"{Name} {nameof(AUnit)} {nameof(SetDestination)} {destination}");

			movementController.DestinationPoint = destination;
			followController.FollowTargetUnit   = null;
			attackController.AttackTargetUnit   = null;
		}

		public virtual void Follow(Transform targetTransform){
			if (useLog) Debug.Log($"{Name} {nameof(AUnit)} {nameof(Follow)} {targetTransform.name}");

			movementController.DestinationPoint    = null;
			followController.FollowTargetTransform = targetTransform;
			attackController.AttackTargetUnit      = null;
		}

		public virtual void Follow(IUnit targetUnit, bool forAttack = false){
			if (useLog)
				Debug.Log($"{Name} {nameof(AUnit)} {nameof(Follow)} {targetUnit.Name}, {nameof(forAttack)}: {forAttack}");

			movementController.DestinationPoint = null;
			followController.FollowTargetUnit   = targetUnit;
			attackController.AttackTargetUnit   = forAttack ? targetUnit : null;
		}

		public virtual void Attack(IUnit targetUnit){
			if (useLog) Debug.Log($"{Name} {nameof(AUnit)} {nameof(Attack)} {targetUnit.Name}");

			movementController.DestinationPoint = null;
			followController.FollowTargetUnit   = null;
			attackController.AttackTargetUnit   = targetUnit;
		}


		[ContextMenu(nameof(Kill))]
		public virtual void Kill(IKillerInfo killerInfo){
			if (useLog) Debug.Log($"{Name} {nameof(AUnit)} {nameof(Kill)} {nameof(killerInfo)}: {killerInfo}");

			OnDeath?.Invoke(killerInfo);

			Destroy();
		}

		[ContextMenu(nameof(Destroy))]
		public virtual void Destroy(){
			OnDestroy?.Invoke();
			Destroy(gameObject);
		}

		public virtual void OnCollisionEnter(Collision collision) => collisionSorter.OnCollisionEnter(collision);

		public virtual void OnTriggerEnter(Collider other) => collisionSorter.OnTriggerEnter(other);


		public virtual void FaceTarget(Vector3 destination, float rotateSpeed = 360.0f, bool lockY = true){
			Vector3 lookPos = destination - Transform.position;

			if (lockY){
				lookPos.y = 0;
			}

			if (lookPos == Vector3.zero){
				return;
			}

			Quaternion rotation = Quaternion.LookRotation(lookPos);
			Transform.rotation = Quaternion.RotateTowards(Transform.rotation, rotation, Time.deltaTime * rotateSpeed);
		}
	}
}