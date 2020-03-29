using Units.Common;
using Units.Player.PlayerStates;
using Units.Player.PlayerStates.States;

namespace Units.Player {
	public class PlayerInstaller : AUnitInstaller {
		public override void InstallBindings(){
			Container.Bind<IPlayer>().To<Player>().FromComponentOnRoot().AsSingle().NonLazy();
			Container.Bind<IUnit>().To<IPlayer>().FromResolve();

			Container.Bind<IPlayerModel>().To<PlayerModel>().FromComponentInHierarchy().AsSingle().NonLazy();
			Container.Bind<IUnitModel>().To<IPlayerModel>().FromResolve();

			Container.BindInterfacesAndSelfTo<TargetRadarController>().FromNew().AsSingle().NonLazy();

			Container.BindInterfacesAndSelfTo<AimController>().FromNew().AsSingle().NonLazy();
			Container.Bind<AimController.Model>().FromResolveGetter<IPlayerModel>(model => model.AimModel);

			Container.BindInterfacesAndSelfTo<PlayerCollisionSorter>().FromNew().AsSingle().NonLazy();

			Container.BindInterfacesAndSelfTo<PlayerStateManager>().FromNew().AsSingle().NonLazy();
			Container.Bind<PlayerStateIdle>().AsSingle().NonLazy();
			Container.Bind<PlayerStateMove>().AsSingle().NonLazy();
			Container.Bind<PlayerStateAttack>().AsSingle().NonLazy();
			Container.Bind<PlayerStateDied>().AsSingle().NonLazy();


			base.InstallBindings();
		}
	}
}