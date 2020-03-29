using UnityEngine;
using Zenject;

namespace Misc.Physical {
	[RequireComponent(typeof(Collider))]
	public class PhysicalCollisionMediator : MonoBehaviour, IPhysical {
		[Inject] private readonly object physicalRoot;
		public object PhysicalRoot => physicalRoot;
	}
}