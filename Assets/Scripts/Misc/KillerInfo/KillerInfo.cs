namespace Misc.KillerInfo {
	public class KillerInfo : IKillerInfo {
		public object Killer{ get; }
		public object Weapon{ get; }

		public KillerInfo(object killer, object weapon){
			Killer = killer;
			Weapon = weapon;
		}

		public override string ToString() =>
			$" {nameof(Killer)}: {Killer?.GetType().Name ?? "null"}  {nameof(Weapon)}: {Weapon?.GetType().Name ?? "null"}";
	}
}