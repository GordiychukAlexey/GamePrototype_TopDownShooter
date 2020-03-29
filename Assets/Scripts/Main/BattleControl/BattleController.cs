using System;
using Main.GUI.HUD;
using Misc;
using Misc.KillerInfo;
using Misc.Physical;
using ModestTree;
using Units.Mobs.Common;
using Units.Player;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Main.BattleControl {
	public class BattleController : MonoBehaviour {
		public event Action OnStartPreparing;
		public event Action OnStartPlaying;
		public event Action<bool> OnStartEnding;
		public event Action<float> OnPreparingTimerUpdate;
		
		[Inject] private readonly UnitsRegistry unitsRegistry;
		[Inject] private readonly GameInstaller.UnitFactoryPlaceholder unitFactory;

		[Inject] private readonly InputAdapter_GroundMovement inputAdapterGroundMovement;
		[Inject] private readonly ScreenSpaceHudSystem screenSpaceHudSystem;

		[SerializeField] private Gate gate;
		[SerializeField] private BattleModel model;

		private float _currentBattlePreparingTime;

		private float CurrentBattlePreparingTime{
			get => _currentBattlePreparingTime;
			set{
				_currentBattlePreparingTime = value;
				OnPreparingTimerUpdate?.Invoke(value);
			}
		}

		protected void OnEnable(){
			unitsRegistry.OnEnemyDie     += EnemyDie;
			unitsRegistry.OnLastEnemyDie += LastEnemyDie;
			unitsRegistry.OnPlayerDie    += PlayerDie;

			gate.TriggerEnter += OnGateTriggerEnter;
		}


		protected void OnDisable(){
			unitsRegistry.OnEnemyDie     -= EnemyDie;
			unitsRegistry.OnLastEnemyDie -= LastEnemyDie;
			unitsRegistry.OnPlayerDie    -= PlayerDie;

			gate.TriggerEnter -= OnGateTriggerEnter;
		}

		private void Awake(){
			model.BattleState = BattleState.WaitToStart;

			gate.gameObject.SetActive(false);
		}

		private void EnemyDie(IKillerInfo killerInfo){
			if (killerInfo.Killer is IPlayer){
				unitsRegistry.Player.AddCoins(model.PlayerRewardForKillEnemy);
			}
		}

		private void LastEnemyDie(){
			gate.gameObject.SetActive(true);
		}

		private void PlayerDie(IKillerInfo killerInfo){
			StartEnding(false);
		}

		private void OnGateTriggerEnter(GameObject other){
			if (other.GetComponent<IPhysical>().PhysicalRoot is IPlayer player){
				player.Destroy();
				StartEnding(true);
			}
		}

		public void StartPreparing(){
			Assert.That(model.BattleState == BattleState.WaitToStart);

			model.BattleState = BattleState.Preparing;

			CurrentBattlePreparingTime = model.BattlePreparingTime;

			{
				IPlayer unit = (IPlayer) unitFactory.Create(model.Player);
				unit.SetPosition(model.PlayerSpawnPoint.position);
				unit.OnCoinsUpdated += screenSpaceHudSystem.SetCoinsValue;

				unitsRegistry.Player = unit;
			}

			for (int i = 0; i < model.EnemiesCount; i++){
				IMob unit = (IMob) unitFactory.Create(model.Enemies[Random.Range(0, model.Enemies.Count)]);
				unit.SetPosition(model.EnemiesSpawnZones[Random.Range(0, model.EnemiesSpawnZones.Count)].GetRandomPointInZone());
				unitsRegistry.AddMob(unit);
			}

			OnStartPreparing?.Invoke();
		}

		public void StartPlaying(){
			Assert.That(model.BattleState == BattleState.Preparing);

			model.BattleState = BattleState.Playing;

			inputAdapterGroundMovement.OnInputValueSet += unitsRegistry.Player.Move;

			foreach (IMob mob in unitsRegistry.Mobs){
				mob.Attack(unitsRegistry.Player);
			}

			OnStartPlaying?.Invoke();
		}

		public void StartEnding(bool isWin){
			Assert.That(model.BattleState == BattleState.Playing);

			model.BattleState = BattleState.Ending;

			inputAdapterGroundMovement.OnInputValueSet -= unitsRegistry.Player.Move;

			OnStartEnding?.Invoke(isWin);
		}

		private void Update(){
			switch (model.BattleState){
				case BattleState.Preparing:
					CurrentBattlePreparingTime -= Time.deltaTime;

					if (CurrentBattlePreparingTime <= 0.0f){
						StartPlaying();
					}

					break;
			}
		}
	}
}