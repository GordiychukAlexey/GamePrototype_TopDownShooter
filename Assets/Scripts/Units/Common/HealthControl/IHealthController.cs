using System;
using Main.BaseController;

namespace Units.Common.HealthControl {
	public interface IHealthController : IController {
		event Action<float> OnHealthUpdated;
		event Action OnHealthIsOver;

		float CurrentHealth{ get; }
		float MaxHealth{ get; }
		void ReceiveDamage(float damage);
	}
}