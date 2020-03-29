using Controls.UnitControls.AttackControls;
using Controls.UnitControls.FollowControls;
using Controls.UnitControls.MovementControls;
using Controls.UnitControls.MovementControls.PathfindingControls;
using Misc.Damageable;
using Misc.Physical;
using Units.Common.HealthControl;
using UnityEngine.AI;
using Zenject;

namespace Units.Common {
	public class AUnitInstaller : MonoInstaller {
		public override void InstallBindings(){
			Container.Bind<IDamageable>().To<IUnit>().FromResolve().NonLazy();
			Container.Bind<IPhysical>().To<IUnit>().FromResolve().NonLazy();

			Container.Bind<object>().To<IUnit>().FromResolve().WhenInjectedInto<PhysicalCollisionMediator>().NonLazy();
			Container.Bind<object>().To<IUnit>().FromResolve().WhenInjectedInto<DamageableCollisionMediator>().NonLazy();

			Container.BindInterfacesAndSelfTo<HealthController>().FromNew().AsSingle().NonLazy();

			Container.Bind<NavMeshAgent>().FromComponentInHierarchy().AsSingle().NonLazy();

			Container.BindInterfacesAndSelfTo<NavMeshPathfindingMovementController>().FromNew().AsSingle().NonLazy();
			Container.BindInterfacesAndSelfTo<UnitAttackController>().FromNew().AsSingle().NonLazy();
			Container.BindInterfacesAndSelfTo<UnitFollowController>().FromNew().AsSingle().NonLazy();

			Container.Bind<HealthModel>().FromResolveGetter<IUnitModel>(model => model.HealthModel);
			Container.Bind<UnitMovementModel>().FromResolveGetter<IUnitModel>(model => model.MovementModel);
			Container.Bind<PathfindingMovementModel>().To<UnitMovementModel>().FromResolve();
			Container.Bind<UnitAttackModel>().FromResolveGetter<IUnitModel>(model => model.AttackModel);
			Container.Bind<UnitFollowModel>().FromResolveGetter<IUnitModel>(model => model.FollowModel);
		}
	}
}