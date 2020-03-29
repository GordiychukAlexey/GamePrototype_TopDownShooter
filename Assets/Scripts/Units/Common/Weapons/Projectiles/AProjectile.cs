using Misc.Damageable;
using UnityEngine;
using Zenject;

namespace Units.Common.Weapons.Projectiles {
	/// <summary>
	/// todo добавить возможность снарядам проходить сквозь врагов
	/// </summary>
	[RequireComponent(typeof(Rigidbody))]
	public abstract class AProjectile : MonoBehaviour, IProjectile, IPoolable<IUnit, IMemoryPool> {
		private const bool useLog = false;

		[SerializeField] private float damage;

		public virtual float Damage{
			get => damage;
			set => damage = value;
		}

		[SerializeField] private float lifetime;

		public virtual float Lifetime{
			get => lifetime;
			set => lifetime = value;
		}

		public IUnit DamageOwner{ get; private set; }

		protected Rigidbody ThisRigidbody{ get; private set; }
		
		private IMemoryPool pool;
		private float spawnTime;

		private void Awake(){
			ThisRigidbody = GetComponent<Rigidbody>();
		}

		public void OnSpawned(IUnit damageOwner, IMemoryPool pool){
			this.pool = pool;

			DamageOwner = damageOwner;

			spawnTime = Time.time;

			OnSpawn();

//			thisRigidbody.isKinematic = false;
		}

		public void OnDespawned(){
			pool = null;

			ThisRigidbody.velocity        = Vector3.zero;
			ThisRigidbody.angularVelocity = Vector3.zero;
//			thisRigidbody.isKinematic     = true;
		}

		protected virtual void OnSpawn(){ }

		private void Update(){
			if (Time.time - spawnTime > Lifetime){
				Destroy();
			}
		}

		public void AppendForce(Vector3 force) => ThisRigidbody.velocity += force;

		public void SetPosition(Vector3 position) => transform.position = position;

		protected virtual void OnCollisionEnter(Collision collision){
			if (useLog) Debug.Log($"{nameof(AProjectile)} {nameof(OnCollisionEnter)} {collision.gameObject.name}");
			IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
			TransmitDamage(damageable);
			Destroy();
		}

		protected virtual void OnTriggerEnter(Collider other){
			if (useLog) Debug.Log($"{nameof(AProjectile)} {nameof(OnTriggerEnter)} {other.gameObject.name}");
			IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
			TransmitDamage(damageable);
			Destroy();
		}

		private void TransmitDamage(IDamageable damageable){
			damageable?.ReceiveDamage(new ProjectileDamageData.ProjectileDamageData(Damage, this, DamageOwner));
		}

		public void Destroy(){
			pool?.Despawn(this);
		}
	}
}