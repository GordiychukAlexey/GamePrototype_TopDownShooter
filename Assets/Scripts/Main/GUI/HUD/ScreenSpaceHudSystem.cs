using UnityEngine;

namespace Main.GUI.HUD {
	public class ScreenSpaceHudSystem : MonoBehaviour {
		[SerializeField] private CoinsHud CoinsHud;

		public void SetCoinsValue(int value) => CoinsHud.SetValue(value);
	}
}