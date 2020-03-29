using Zenject;

namespace Units.Common.Weapons.ConcreteWeapons.PlayerWeapons {
	public class PlayerWeaponInstaller : MonoInstaller {
		public override void InstallBindings(){
			Container.Bind<IPlayerWeapon>().To<PlayerWeapons.PlayerWeapon>().FromComponentInHierarchy().AsSingle().NonLazy();
			Container.Bind<PlayerWeaponModel>().FromComponentInHierarchy().AsSingle().NonLazy();
		}
	}
}