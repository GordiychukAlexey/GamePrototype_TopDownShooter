using Zenject;

namespace Main.BaseController {
	public abstract class AController : IController, IInitializable, ITickable, IFixedTickable {
		private bool isActive = true;

		public bool IsActive{
			get => isActive;
			set{
				if (isActive != value){
					isActive = value;

					if (isActive){
						OnEnable();
					} else{
						OnDisable();
					}
				}
			}
		}

		public void Initialize(){
			Awake();
			OnEnable();
		}

		protected virtual void Awake(){ }

		protected virtual void OnEnable(){ }
		protected virtual void OnDisable(){ }

//		protected virtual void Start(){ }

		protected virtual void Update(){ }
		protected virtual void FixedUpdate(){ }

		public virtual void Reset(){ }

		public void Tick(){
			if (!IsActive){
				return;
			}

			Update();
		}

		public void FixedTick(){
			if (!IsActive){
				return;
			}

			FixedUpdate();
		}
	}
}