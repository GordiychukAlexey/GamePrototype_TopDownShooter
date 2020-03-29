using System;
using UnityEngine;

namespace Units.Common.HealthControl {
	[Serializable]
	public class HealthModel {
		[SerializeField] private float maxHealth;

		public float MaxHealth{
			get => maxHealth;
			set => maxHealth = value;
		}

		public float CurrentHealth{ get; set; }

		public bool IsDead => CurrentHealth <= 0.0f;
	}
}