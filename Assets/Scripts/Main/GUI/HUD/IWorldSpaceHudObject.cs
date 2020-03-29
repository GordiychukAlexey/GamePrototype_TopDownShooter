using System;
using UnityEngine;

namespace Main.GUI.HUD {
	public interface IWorldSpaceHudObject : IHudObject {
		Func<Vector3> LocatorPosition{ get; set; }

		void Destroy();
	}
}