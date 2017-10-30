using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace RoguePlateformer {
    public class Minimap : MonoBehaviour {

        public Camera minimapCam;
        public Transform mainCam;

        public void Update() {
            if (!(SceneManager.GetActiveScene().name == "Menu" || SceneManager.GetActiveScene().name == "Shop")) {
                if (Input.GetButton("Map")) {
                    GameObject.Find("Minimap").GetComponent<Mask>().showMaskGraphic = true;
                } else {
                    GameObject.Find("Minimap").GetComponent<Mask>().showMaskGraphic = false;
                }
            }
        }

        public void LateUpdate() {
            if (mainCam != null) {
                minimapCam.transform.position = new Vector3(mainCam.position.x, mainCam.position.y, -20f);
            }
        }

    }
}
