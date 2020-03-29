using System;
using Main.BaseController;
using Zenject;

namespace Units.Common.HealthControl {
	public class HealthController : AController, IHealthController {
		public event Action<float> OnHealthUpdated;
		public event Action OnHealthIsOver;

		[Inject] private readonly HealthModel healthModel;

		protected override void Awake(){
			Reset();
		}

		public override void Reset(){
			healthModel.CurrentHealth = healthModel.MaxHealth;
		}

		public float CurrentHealth => healthModel.CurrentHealth;

		public float MaxHealth => healthModel.MaxHealth;

		public void ReceiveDamage(float damage){
			float newHealth = healthModel.CurrentHealth - damage;
			if (newHealth < 0.0f){
				newHealth = 0.0f;
			}

			healthModel.CurrentHealth = newHealth;
			OnHealthUpdated?.Invoke(healthModel.CurrentHealth);

//			Debug.Log($"{nameof(HealthController)} {nameof(ReceiveDamage)} {damage}, health: {healthModel.CurrentHealth}");

			if (healthModel.CurrentHealth == 0.0f){
				OnHealthIsOver?.Invoke();
			}
		}
	}
}