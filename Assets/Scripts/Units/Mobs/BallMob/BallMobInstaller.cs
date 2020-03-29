using Units.Mobs.Common;

namespace Units.Mobs.BallMob {
	public class BallMobInstaller : AMobInstaller {
		public override void InstallBindings(){
			Container.Bind<IBallMob>().To<BallMob>().FromComponentOnRoot().AsSingle().NonLazy();
			Container.Bind<IMob>().To<IBallMob>().FromResolve();

			Container.Bind<BallMobModel>().FromComponentInHierarchy().AsSingle().NonLazy();
			Container.Bind<IMobModel>().To<BallMobModel>().FromResolve();

			base.InstallBindings();
		}
	}
}