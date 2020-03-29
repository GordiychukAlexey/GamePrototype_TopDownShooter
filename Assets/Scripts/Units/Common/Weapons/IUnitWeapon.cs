namespace Units.Common.Weapons {
	public interface IUnitWeapon : IWeapon {
		IUnit Owner{ get; set; }
	}
}