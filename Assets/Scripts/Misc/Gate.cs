using System;
using UnityEngine;

namespace Misc {
	public class Gate : MonoBehaviour {
		public event Action<GameObject> TriggerEnter;

		private void OnTriggerEnter(Collider other){
			TriggerEnter?.Invoke(other.gameObject);
		}
	}
}