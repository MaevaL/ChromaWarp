using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public GameObject player;

    public float OffSetMoveCam;

    // LateUpdate is called after Update each frame
    void Update() {

        if (player == null) { return; }

        //Verification et decalage à droite si necessaire.
        if (player.transform.position.x - transform.position.x > OffSetMoveCam) {
            transform.position += new Vector3(2 * OffSetMoveCam, 0, 0);

        }
        //Verification et decalage à gauche si necessaire.
        else if (player.transform.position.x - transform.position.x < -OffSetMoveCam) {
            transform.position += new Vector3(-2 * OffSetMoveCam, 0, 0);
        }
    }
}