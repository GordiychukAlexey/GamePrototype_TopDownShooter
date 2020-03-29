using UnityEngine;
using UnityEngine.UI;

namespace Units.Common.HUD {
	public class HealthBar : MonoBehaviour {
		public Image HealthValueImage;

		public float Health{
			set => HealthValueImage.fillAmount = Mathf.Clamp01(value);
		}
	}
}