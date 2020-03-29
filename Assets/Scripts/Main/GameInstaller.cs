using Controls.InputControls;
using Main.BattleControl;
using Main.GUI;
using Main.GUI.HUD;
using Main.Spawners;
using Units.Common;
using Units.Common.HUD;
using Units.Common.Weapons.Projectiles.ConcreteProjectiles;
using UnityEngine;
using Zenject;

namespace Main {
	public class GameInstaller : MonoInstaller {
		public CompositionRoot CompositionRoot;
		public BattleController BattleController;
		public WorldSpaceHudSystem WorldSpaceHudSystem;
		public PlayerWeaponBullet PlayerWeaponBulletPrefab;
		public MobWeaponBullet MobWeaponBulletPrefab;
		public InGameGui InGameGui;
		public ScreenSpaceHudSystem ScreenSpaceHudSystem;
		public UnitWorldSpaceHud UnitWorldSpaceHudPrefab;

		public override void InstallBindings(){
			Container.Bind<CompositionRoot>().FromInstance(CompositionRoot).AsSingle().NonLazy();

			Container.Bind<BattleController>().FromInstance(BattleController).AsSingle().NonLazy();

			Container.Bind<InGameGui>().FromInstance(InGameGui).AsSingle().NonLazy();
			Container.Bind<ScreenSpaceHudSystem>().FromInstance(ScreenSpaceHudSystem).AsSingle().NonLazy();
			Container.Bind<WorldSpaceHudSystem>().FromInstance(WorldSpaceHudSystem).AsSingle().NonLazy();

			Container.Bind<UnitsRegistry>().FromNew().AsSingle().NonLazy();

			Container.Bind(typeof(IInputController), typeof(ITickable)).To<InputController>().FromNew().AsSingle().NonLazy();
			Container.BindInterfacesAndSelfTo<InputAdapter_GroundMovement>().FromNew().AsSingle().NonLazy();

			Transform PoolsRoot = Container.CreateEmptyGameObject(nameof(PoolsRoot)).transform;
			Container.Bind<Transform>().WithId(InjectId.PoolsRoot).FromInstance(PoolsRoot).AsSingle().NonLazy();
			Transform PlayerBulletsPool = Container.CreateEmptyGameObject(nameof(PlayerBulletsPool)).transform;
			PlayerBulletsPool.parent = PoolsRoot;
			Transform MobsBulletsPool = Container.CreateEmptyGameObject(nameof(MobsBulletsPool)).transform;
			MobsBulletsPool.parent = PoolsRoot;


			Container.BindFactory<GameObject, IUnit, UnitFactoryPlaceholder>().FromFactory<UnitFactory>().NonLazy();

			Container.BindFactory<IUnit, PlayerWeaponBullet, PlayerWeaponBullet.Factory>()
					 .FromPoolableMemoryPool<IUnit, PlayerWeaponBullet, PlayerWeaponBulletPool>(
						 poolBinder => poolBinder
									   .WithInitialSize(4)
									   .FromComponentInNewPrefab(PlayerWeaponBulletPrefab)
									   .UnderTransform(PlayerBulletsPool));

			Container.BindFactory<IUnit, MobWeaponBullet, MobWeaponBullet.Factory>()
					 .FromPoolableMemoryPool<IUnit, MobWeaponBullet, MobWeaponBulletPool>(
						 poolBinder => poolBinder
									   .WithInitialSize(6)
									   .FromComponentInNewPrefab(MobWeaponBulletPrefab)
									   .UnderTransform(MobsBulletsPool));

			Container.BindFactory<UnitWorldSpaceHud, UnitWorldSpaceHud.Factory>()
					 .FromPoolableMemoryPool<UnitWorldSpaceHud, UnitHudPool>(
						 poolBinder => poolBinder
									   .WithInitialSize(6)
									   .FromComponentInNewPrefab(UnitWorldSpaceHudPrefab)
									   .UnderTransform(WorldSpaceHudSystem.transform));
		}

		class PlayerWeaponBulletPool : MonoPoolableMemoryPool<IUnit, IMemoryPool, PlayerWeaponBullet> { }

		class MobWeaponBulletPool : MonoPoolableMemoryPool<IUnit, IMemoryPool, MobWeaponBullet> { }

		class UnitHudPool : MonoPoolableMemoryPool<IMemoryPool, UnitWorldSpaceHud> { }

		public class UnitFactoryPlaceholder : PlaceholderFactory<GameObject, IUnit> { }

		public enum InjectId {
			PoolsRoot,
		}
	}
}