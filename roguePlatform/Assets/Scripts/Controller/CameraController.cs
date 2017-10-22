using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public GameObject player;       //Public variable to store a reference to the player game object

    public float OffSetMoveCam;
    //private Vector3 offset;         //Private variable to store the offset distance between the player and camera

    // Use this for initialization
    void Start()
    {
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
       // offset = transform.position - player.transform.position;
    }

    // LateUpdate is called after Update each frame
    void Update() {

        if(player == null) {
            return;
        }

        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        //transform.position = player.transform.position + offset;

        //Verification et decalage a droite si necessaire.
        if (player.transform.position.x - transform.position.x > OffSetMoveCam) {
            transform.position += new Vector3(2 * OffSetMoveCam, 0, 0);

        }
        //Verification et decalage a gauche si necessaire.
        else if (player.transform.position.x - transform.position.x < -OffSetMoveCam) {
            transform.position += new Vector3(- 2 * OffSetMoveCam, 0, 0);
        }
    }
}