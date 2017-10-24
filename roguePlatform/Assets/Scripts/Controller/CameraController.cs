using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public GameObject player;

    public float OffSetMoveCamX;
    public float OffSetMoveCamY; 

    // LateUpdate is called after Update each frame
    void Update() {

        if (player == null) { return; }

        //Verification et decalage à droite si necessaire.
        if (player.transform.position.x - transform.position.x > OffSetMoveCamX) {
            transform.position += new Vector3(2 * OffSetMoveCamX, 0, 0);

        }
        //Verification et decalage à gauche si necessaire.
        else if (player.transform.position.x - transform.position.x < -OffSetMoveCamX) {
            transform.position += new Vector3(-2 * OffSetMoveCamX, 0, 0);
        }

        if (player.transform.position.y - transform.position.y > OffSetMoveCamY)
        {
            transform.position += new Vector3(0, 2 * OffSetMoveCamY, 0);

        }
        //Verification et decalage à gauche si necessaire.
        else if (player.transform.position.y - transform.position.y < -OffSetMoveCamY)
        {
            transform.position += new Vector3(0, -2 * OffSetMoveCamY, 0);
        }
    }
}