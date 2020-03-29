using Zenject;

namespace Units.Common.Weapons.ConcreteWeapons.MobWeapons {
	public class MobWeaponInstaller : MonoInstaller {
		public override void InstallBindings(){
			Container.Bind<IMobWeapon>().To<MobWeapon>().FromComponentInHierarchy().AsSingle().NonLazy();
			Container.Bind<MobWeaponModel>().FromComponentInHierarchy().AsSingle().NonLazy();
		}
	}
}