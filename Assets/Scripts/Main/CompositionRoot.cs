using Main.BattleControl;
using Main.GUI;
using UnityEngine;
using Zenject;

namespace Main {
	public class CompositionRoot : MonoBehaviour {
		[Inject] private readonly InGameGui inGameGui;
		[Inject] private BattleController battleController;

		private void OnEnable(){
			battleController.OnStartPreparing       += OnStartPreparing;
			battleController.OnStartPlaying         += OnStartPlaying;
			battleController.OnStartEnding          += OnStartEnding;
			battleController.OnPreparingTimerUpdate += PreparingTimerUpdate;
		}

		private void OnDisable(){
			battleController.OnStartPreparing       -= OnStartPreparing;
			battleController.OnStartPlaying         -= OnStartPlaying;
			battleController.OnStartEnding          -= OnStartEnding;
			battleController.OnPreparingTimerUpdate -= PreparingTimerUpdate;
		}

		private void Start(){
			battleController.StartPreparing();
		}

		private void PreparingTimerUpdate(float value) => inGameGui.SetPreparingTimerValue((int) Mathf.Ceil(value));

		private void OnStartPreparing() => inGameGui.SetPreparingTimerActive(true);

		private void OnStartPlaying() => inGameGui.SetPreparingTimerActive(false);

		private void OnStartEnding(bool isWin) => inGameGui.GameEnd(isWin);
	}
}