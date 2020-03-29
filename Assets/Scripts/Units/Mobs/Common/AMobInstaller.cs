using Controls.CollisionControl;
using Units.Common;
using Units.Mobs.Common.AI;
using Units.Mobs.Common.DamageProvisionControl;
using Units.Mobs.Common.MobStates;
using Units.Mobs.Common.MobStates.States;

namespace Units.Mobs.Common {
	public abstract class AMobInstaller : AUnitInstaller {
		public override void InstallBindings(){
			Container.Bind<IUnit>().To<IMob>().FromResolve();

			Container.Bind<IUnitModel>().To<IMobModel>().FromResolve();

			Container.BindInterfacesAndSelfTo<MobAIMovementController>().FromNew().AsSingle().NonLazy();
			Container.Bind<MobAIMovementModel>().FromResolveGetter<IMobModel>(model => model.AiMovementModel);

			Container.BindInterfacesAndSelfTo<CollisionSorter>().FromNew().AsSingle().NonLazy();

			Container.BindInterfacesAndSelfTo<MobDamageProvider>().FromNew().AsSingle().NonLazy();
			Container.BindInterfacesAndSelfTo<MobDamageProvisionModel>()
					 .FromResolveGetter<IMobModel, MobDamageProvisionModel>(model => model.DamageProvisionModel);

			Container.BindInterfacesAndSelfTo<MobStateManager>().FromNew().AsSingle().NonLazy();
			Container.Bind<MobStateIdle>().AsSingle().NonLazy();
			Container.Bind<MobStateFollow>().AsSingle().NonLazy();
			Container.Bind<MobStateAttack>().AsSingle().NonLazy();

			base.InstallBindings();
		}
	}
}