using UnityEngine;

namespace RoguePlateformer {
	public class Minimap : MonoBehaviour {

		public RenderTexture renderTexture;
		public Camera minimapCam;
		public Transform player;

		public void Start() {
			
		}

		public void LateUpdate() {
			minimapCam.transform.position = player.position + Vector3.forward * -10;
		}


	}
}
