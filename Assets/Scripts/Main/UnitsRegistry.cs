using System;
using System.Collections.Generic;
using Misc.KillerInfo;
using Units.Mobs.Common;
using Units.Player;
using UnityEngine;

namespace Main {
	public class UnitsRegistry {
		public event Action<IKillerInfo> OnPlayerDie;
		public event Action<IKillerInfo> OnEnemyDie;
		public event Action OnLastEnemyDie;

		private IPlayer player;

		public IPlayer Player{
			get => player;
			set{
				if (player != null){
					player.OnDeath -= OnPlayerDie;
				}

				player         =  value;
				player.OnDeath += OnPlayerDie;
			}
		}


		private readonly Dictionary<IMob, MobEventsHandlers> mobs = new Dictionary<IMob, MobEventsHandlers>();
		public IReadOnlyCollection<IMob> Mobs => mobs.Keys;

		private struct MobEventsHandlers {
			public Action<IKillerInfo> KillHandler;
		}

		public void AddMob(IMob mob){
			if (mobs.ContainsKey(mob)){
				Debug.LogError("Already registered");
				return;
			}

			void KillAction(IKillerInfo killerInfo) => MobDeath(mob, killerInfo);
			mobs.Add(mob, new MobEventsHandlers(){
				KillHandler = KillAction,
			});
			mob.OnDeath += KillAction;
//			Debug.Log("Mob added");
		}

		public void RemoveMob(IMob mob){
			if (mobs.ContainsKey(mob)){
				mob.OnDeath -= mobs[mob].KillHandler;
				mobs.Remove(mob);
//				Debug.Log("Mob removed");
			} else{
				throw new ArgumentException("Unknown unit");
			}
		}

		private void MobDeath(IMob mob, IKillerInfo killerInfo){
			OnEnemyDie?.Invoke(killerInfo);
			RemoveMob(mob);
			if (mobs.Count == 0){
				OnLastEnemyDie?.Invoke();
			}
		}
	}
}