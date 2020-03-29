using UnityEngine;
using UnityEngine.UI;

namespace Main.GUI {
	public class BattlePreparingTimer : MonoBehaviour {
		[SerializeField] private Text preparingTimerValue;

		public void SetValue(int value) => preparingTimerValue.text = value.ToString();
	}
}