namespace Units.Common.Weapons {
	public abstract class AUnitWeapon : AWeapon, IUnitWeapon {
		protected abstract UnitWeaponModel UnitWeaponModel{ get; }

		public IUnit Owner{
			get => UnitWeaponModel.Owner;
			set => UnitWeaponModel.Owner = value;
		}

		protected override AWeaponModel WeaponModel => UnitWeaponModel;
	}
}