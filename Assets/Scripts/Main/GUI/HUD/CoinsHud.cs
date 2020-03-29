using UnityEngine;
using UnityEngine.UI;

namespace Main.GUI.HUD {
	public class CoinsHud : MonoBehaviour {
		[SerializeField] private Text coinsCountValue;

		public void SetValue(int value) => coinsCountValue.text = value.ToString();
	}
}