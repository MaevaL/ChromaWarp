using UnityEngine;
using System.Collections;

/// <summary>
///  Camera controller
///  Manage the camera of the scene
/// </summary>
public class CameraController : MonoBehaviour {

    public GameObject player;
    public float OffSetMoveCamX;
    public float OffSetMoveCamY;


    void Update() {
        if (player == null) { return; }
        //Check the player position and change screen to right when necessary
        if (player.transform.position.x - transform.position.x > OffSetMoveCamX) {
            transform.position += new Vector3(2 * OffSetMoveCamX, 0, 0);
        }
        //Check the player position and change screen to left when necessary
        else if (player.transform.position.x - transform.position.x < -OffSetMoveCamX) {
            transform.position += new Vector3(-2 * OffSetMoveCamX, 0, 0);
        }
        //Check the player position and change screen to top when necessary
        if (player.transform.position.y - transform.position.y > OffSetMoveCamY) {
            transform.position += new Vector3(0, 2 * OffSetMoveCamY, 0);
        }
        //Check the player position and change screen to bottom when necessary
        else if (player.transform.position.y - transform.position.y < -OffSetMoveCamY) {
            transform.position += new Vector3(0, -2 * OffSetMoveCamY, 0);
        }
    }
}