using System;
using Units.Common;

namespace Units.Player {
	public interface IPlayer : IUnit {
		event Action<int> OnCoinsUpdated;
		IPlayerModel Model{ get; } //todo REMOVE
		void AddCoins(int value);
	}
}