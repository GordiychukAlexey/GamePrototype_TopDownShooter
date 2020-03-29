using System;
using System.Linq;
using Main;
using Main.BaseController;
using Units.Mobs.Common;
using UnityEngine;
using Zenject;

namespace Units.Player {
	public class TargetRadarController : AController {
		public event Action<IMob> ClosestMobUpdated;

		[Inject] private UnitsRegistry unitsRegistry;

		[Inject] private IPlayer player;

		protected override void Update(){
			IMob closestMob = unitsRegistry.Mobs.Where(mob => !mob.IsDead)
									  .OrderBy(mob => Vector3.Distance(player.BodyCenterPosition, mob.BodyCenterPosition))
									  .FirstOrDefault();

			ClosestMobUpdated?.Invoke(closestMob);
		}
	}
}