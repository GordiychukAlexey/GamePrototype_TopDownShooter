using UnityEngine;
using UnityEngine.Serialization;

namespace Main.GUI {
	public class InGameGui : MonoBehaviour {
		[FormerlySerializedAs("preparingTimer")]
		[SerializeField]
		private BattlePreparingTimer battlePreparingTimer;

		[SerializeField] private RectTransform winLogo;
		[SerializeField] private RectTransform loseLogo;


		public void Awake(){
			SetPreparingTimerActive(false);

			winLogo.gameObject.SetActive(false);
			loseLogo.gameObject.SetActive(false);
		}

		public void SetPreparingTimerValue(int value) => battlePreparingTimer.SetValue(value);

		public void SetPreparingTimerActive(bool isActive) => battlePreparingTimer.gameObject.SetActive(isActive);

		public void GameEnd(bool isWin){
			winLogo.gameObject.SetActive(isWin);
			loseLogo.gameObject.SetActive(!isWin);
		}
	}
}