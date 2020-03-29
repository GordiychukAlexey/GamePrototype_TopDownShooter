using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Main.GUI.HUD {
	[RequireComponent(typeof(RectTransform))]
	public class WorldSpaceHudSystem : MonoBehaviour {
		[Inject(Id = "MainCamera")] private readonly Camera mainCamera;

		private readonly HashSet<IWorldSpaceHudObject> worldSpaceHudObjects = new HashSet<IWorldSpaceHudObject>();

		public void AddHudObject(IWorldSpaceHudObject hudObject){
			worldSpaceHudObjects.Add(hudObject);
		}

		public void RemoveHudObject(IWorldSpaceHudObject hudObject){
			worldSpaceHudObjects.Remove(hudObject);
//			Destroy(hudObject);
		}

		private void Update(){
			foreach (IWorldSpaceHudObject hud in worldSpaceHudObjects){
				hud.RectTransform.position =
					mainCamera.WorldToScreenPoint(hud.LocatorPosition.Invoke());
			}
		}
	}
}