using System;
using Misc.Damageable;
using Misc.KillerInfo;
using Misc.Physical;
using UnityEngine;

namespace Units.Common {
	public interface IUnit : IDamageable, IPhysical {
		event Action<float> OnHealthUpdated;
		event Action<IKillerInfo> OnDeath;
		event Action OnDestroy;

		string Name{ get; }
		float MaxHealth{ get; }
		Transform Transform{ get; }

		Transform BodyCenterLocator{ get; }
		Vector3 BodyCenterPosition{ get; }

		Vector3 HudLocatorPosition{ get; }

		bool IsDead{ get; }

		void SetPosition(Vector3 position);
		void Move(Vector3 moveValue);
		void Stop();
		void SetDestination(Vector3 destination);
		void Follow(Transform targetTransform);
		void Follow(IUnit targetUnit, bool forAttack = false);
		void Attack(IUnit targetUnit);
		void FaceTarget(Vector3 destination, float rotateSpeed = 360.0f, bool lockY = true);
		void Kill(IKillerInfo killerInfo);
		void Destroy();
	}
}