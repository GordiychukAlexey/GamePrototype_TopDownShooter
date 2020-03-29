using System;
using UnityEngine;
using Zenject;

namespace Units.Common.HUD {
	//todo view
	[RequireComponent(typeof(RectTransform))]
	public class UnitWorldSpaceHud : MonoBehaviour, IUnitWorldSpaceHudObject, IPoolable<IMemoryPool> {
		public HealthBar healthBar;

		public Func<Vector3> LocatorPosition{ get; set; }
		
		public RectTransform RectTransform => rectTransform;

		private RectTransform rectTransform;

		public void UnitHealthUpdated(float health) => healthBar.Health = health;

		private void Start(){
			rectTransform = GetComponent<RectTransform>();
		}

		public void Destroy() => pool.Despawn(this);

		private IMemoryPool pool;

		public void OnSpawned(IMemoryPool pool){
			this.pool = pool;
		}

		public void OnDespawned(){
			pool = null;
		}
		
		public class Factory : PlaceholderFactory<UnitWorldSpaceHud> { }
	}
}