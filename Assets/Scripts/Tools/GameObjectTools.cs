using UnityEngine;

namespace Tools {
	public static class GameObjectTools {
		public static GameObject SpawnChildGameObject(string name, Transform parent) =>
			SpawnChildGameObject(name, parent, Vector3.zero, Quaternion.identity);

		public static GameObject SpawnChildGameObject(string name, Transform parent, Vector3 relativePosition, Quaternion relativeRotation){
			GameObject childObj = new GameObject(name);

			childObj.transform.parent        = parent;
			childObj.transform.localPosition = relativePosition;
			childObj.transform.localRotation = relativeRotation;
			childObj.transform.localScale    = Vector3.one;

			return childObj;
		}
	}
}