using System;
using System.Collections.Generic;
using Misc;
using UnityEngine;

namespace Main.BattleControl {
	[Serializable]
	public class BattleModel {
		[SerializeField] private float battlePreparingTime = 3;
		[SerializeField] private int enemiesCount = 5;
		[SerializeField] private GameObject player;
		[SerializeField] private List<GameObject> enemies;
		[SerializeField] private Transform playerSpawnPoint;
		[SerializeField] private List<SpawnZone> enemiesSpawnZones;
		[SerializeField] private int playerRewardForKillEnemy = 10;

		public float BattlePreparingTime{
			get => battlePreparingTime;
			set => battlePreparingTime = value;
		}


		public int EnemiesCount{
			get => enemiesCount;
			set => enemiesCount = value;
		}

		public GameObject Player => player;
		public Transform PlayerSpawnPoint => playerSpawnPoint;
		public List<GameObject> Enemies => enemies;
		public List<SpawnZone> EnemiesSpawnZones => enemiesSpawnZones;
		public int PlayerRewardForKillEnemy => playerRewardForKillEnemy;

		public BattleState BattleState{ get; set; }
	}

	public enum BattleState {
		WaitToStart = 0,
		Preparing,
		Playing,
		Ending,
	}
}