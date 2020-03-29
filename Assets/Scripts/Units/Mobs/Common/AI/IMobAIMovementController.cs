using Main.BaseController;

namespace Units.Mobs.Common.AI {
	public interface IMobAIMovementController : IController {
		MobAIMovementModel.MovementStage CurrentMovementStage{ get; }
	}
}