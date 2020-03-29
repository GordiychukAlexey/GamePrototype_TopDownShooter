using Units.Common;

namespace Units.Mobs.Common {
	public interface IMob : IUnit {
		IMobModel Model{ get; }
	}
}