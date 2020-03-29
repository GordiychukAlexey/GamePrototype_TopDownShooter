using System;
using Misc.Pickable;
using Units.Common;
using Units.Common.Weapons.ConcreteWeapons.PlayerWeapons;
using Units.Player.PlayerStates;
using UnityEngine;
using Zenject;

namespace Units.Player {
	public class Player : AUnit, IPlayer {
		private const bool useLog = false;

		public event Action<int> OnCoinsUpdated;

		[Inject] private IPlayerModel model;
		[Inject] private PlayerStateManager stateManager;
		[Inject] private IPlayerWeapon weapon;

		[Inject] private new IPlayerCollisionSorter collisionSorter;

		public IPlayerModel Model => model;
		
		private void Awake(){
			weapon.Owner = this;
		}

		protected override void OnEnable(){
			collisionSorter.OnCollisionWithPickable += CollisionWithPickable;

			base.OnEnable();
		}

		protected override void OnDisable(){
			base.OnDisable();

			collisionSorter.OnCollisionWithPickable -= CollisionWithPickable;
		}

		private void CollisionWithPickable(IPickable obj){
			throw new NotImplementedException();
		}

		public void AddCoins(int value){
			if (value < 0){
				throw new ArgumentException("Try Add negative coins value");
			}

			model.CoinsCount += value;
			OnCoinsUpdated?.Invoke(model.CoinsCount);
		}

		public override void Stop(){
			if (useLog) Debug.Log($"{Name} {nameof(Player)} {nameof(Stop)}");

			base.Stop();

			stateManager.ChangeState(PlayerStates.PlayerStates.Idle);
		}

		public override void Move(Vector3 moveValue){
			base.Move(moveValue);

			if (moveValue != Vector3.zero){
				stateManager.ChangeState(PlayerStates.PlayerStates.Move);
			} else if (stateManager.CurrentState == PlayerStates.PlayerStates.Move){
				base.Stop();
				stateManager.ChangeState(PlayerStates.PlayerStates.Idle);
			}
		}

		public override void Attack(IUnit targetUnit){
			if (useLog) Debug.Log($"{Name} {nameof(Player)} {nameof(Attack)} {targetUnit.Transform.name}");

			base.Attack(targetUnit);

			stateManager.ChangeState(PlayerStates.PlayerStates.Attack);
		}
	}
}