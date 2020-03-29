using Units.Mobs.Common;

namespace Units.Mobs.BoxMob {
	public class BoxMobInstaller : AMobInstaller {
		public override void InstallBindings(){
			Container.Bind<IBoxMob>().To<BoxMob>().FromComponentOnRoot().AsSingle().NonLazy();
			Container.Bind<IMob>().To<IBoxMob>().FromResolve();

			Container.Bind<BoxMobModel>().FromComponentInHierarchy().AsSingle().NonLazy();
			Container.Bind<IMobModel>().To<BoxMobModel>().FromResolve();

			base.InstallBindings();
		}
	}
}