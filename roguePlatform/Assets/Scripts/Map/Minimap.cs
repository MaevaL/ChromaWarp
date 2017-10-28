using UnityEngine;

namespace RoguePlateformer {
	public class Minimap : MonoBehaviour {
		
		public Camera minimapCam;
		public Transform mainCam;

		public void Start() {
			
		}

		public void LateUpdate() {
			minimapCam.transform.position = new Vector3(mainCam.position.x, mainCam.position.y, -20f);
			//Vector3 fake = (Vector2) MapManager.I.Grid.GetChunkAtWorldPos(mainCam.position).FakePos;
			//fake.x = fake.x * MapManager.I.chunkWidth + MapManager.I.chunkWidth / 2;
			//fake.y = fake.y * MapManager.I.chunkHeight + MapManager.I.chunkHeight / 2;
			//fake.z = -20f;
			//minimapCam.transform.position = fake;
		}


	}
}
