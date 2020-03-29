using System;
using Controls.CollisionControl;
using Misc.Pickable;
using UnityEngine;

namespace Units.Player {
	public interface IPlayerCollisionSorter : ICollisionSorter {
		event Action<IPickable> OnCollisionWithPickable;
	}

	public class PlayerCollisionSorter : CollisionSorter, IPlayerCollisionSorter {
		public event Action<IPickable> OnCollisionWithPickable;

		protected override void Collision(GameObject other){
			base.Collision(other);

			IPickable pickable = other.GetComponent<IPickable>();
			if (pickable != null){
				OnCollisionWithPickable?.Invoke(pickable);
			}
		}
	}
}