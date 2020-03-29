using Main.GUI.HUD;

namespace Units.Common.HUD {
	public interface IUnitWorldSpaceHudObject : IWorldSpaceHudObject {
		void UnitHealthUpdated(float health);
	}
}