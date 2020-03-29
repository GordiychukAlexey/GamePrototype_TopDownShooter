using Main.GUI.HUD;
using Units.Common;
using Units.Common.HUD;
using UnityEngine;
using Zenject;

namespace Main.Spawners {
	public class UnitFactory : IFactory<GameObject, IUnit> {
		[Inject] private readonly WorldSpaceHudSystem worldSpaceHudSystem;
		[Inject] private readonly UnitWorldSpaceHud.Factory unitHudFactory;

		private readonly DiContainer container;
		private readonly Transform UnitsRoot;

		public UnitFactory(DiContainer container,
						   [Inject(Id = GameInstaller.InjectId.PoolsRoot)]
						   Transform poolsRoot){
			this.container = container;

			UnitsRoot        = new GameObject(nameof(UnitsRoot)).transform;
			UnitsRoot.parent = poolsRoot;
		}

		public IUnit Create(GameObject unitPrefab){
			IUnit unit = container.InstantiatePrefabForComponent<IUnit>(unitPrefab, UnitsRoot);

			IUnitWorldSpaceHudObject unitWorldSpaceHudObject = unitHudFactory.Create();
			unitWorldSpaceHudObject.LocatorPosition = () => unit.HudLocatorPosition;

			worldSpaceHudSystem.AddHudObject(unitWorldSpaceHudObject);

			unit.OnHealthUpdated += (float health) => unitWorldSpaceHudObject.UnitHealthUpdated(health / unit.MaxHealth);
			unit.OnDestroy += () => {
				worldSpaceHudSystem.RemoveHudObject(unitWorldSpaceHudObject);
				unitWorldSpaceHudObject.Destroy();
			};

			return unit;
		}
	}
}