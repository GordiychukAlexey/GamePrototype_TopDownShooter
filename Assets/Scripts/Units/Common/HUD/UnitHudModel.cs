using System;
using UnityEngine;

namespace Units.Common.HUD {
	[Serializable]
	public class UnitHudModel {
		[SerializeField] private Transform hudLocator;

		public Transform HudLocator{
			get => hudLocator;
			set => hudLocator = value;
		}
	}
}