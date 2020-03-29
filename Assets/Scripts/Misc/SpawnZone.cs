using UnityEngine;
using Random = UnityEngine.Random;

namespace Misc {
	public class SpawnZone : MonoBehaviour {
		private const float twoPi = 2.0f * Mathf.PI;

		[Min(0.0f)] public float Radius = 5.0f;


		public Vector3 GetRandomPointInZone(){
			Vector2 insideCircle = Random.insideUnitCircle * Radius;
			return transform.position + new Vector3(insideCircle.x, 0.0f, insideCircle.y);
		}


		private void OnDrawGizmos(){
			Gizmos.color = new Color(1.0f, 1.0f, 1.0f, 0.75f);
			Ellipse(transform.position, Vector2.one * Radius, Mathf.Clamp((int) (Mathf.PI * Radius) * 4, 16, 40));
		}

		private static void Ellipse(Vector3 position, Vector2 radius, int segments){
			Vector2 posNMinusOne = Vector2.zero;

			float angle = 0f;


			for (int i = 0; i < (segments + 1); i++){
				Vector2 posN = new Vector2(
					Mathf.Sin(angle) * radius.x,
					Mathf.Cos(angle) * radius.y
				);

				Vector3 vo = new Vector3(position.x + posNMinusOne.x, position.y, position.z + posNMinusOne.y);
				Vector3 vn = new Vector3(position.x + posN.x, position.y, position.z + posN.y);

				if (i > 0) Gizmos.DrawLine(vo, vn);
//				if (i > 0) Gizmos.DrawLine(position, vn);

				posNMinusOne =  posN;
				angle        += twoPi / segments;
			}
		}
	}
}