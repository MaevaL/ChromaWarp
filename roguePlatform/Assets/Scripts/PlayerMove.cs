using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {
    [SerializeField]
    private float speed;
    private Rigidbody2D rigidBody;
    private bool isFlying;
    private int nbJump;



    // Use this for initialization
    void Start() {
        speed = 0.08f;
        rigidBody = GetComponent<Rigidbody2D>();
        isFlying = false;
        nbJump = 0;

    }

    // Update is called once per frame
    void FixedUpdate() {
        if (Input.GetKey(KeyCode.UpArrow)) {
            transform.position += new Vector3(0, 0, speed);
        }

        if (Input.GetKey(KeyCode.DownArrow)) {
            transform.position -= new Vector3(0, 0, speed);
        }

        if (Input.GetKey(KeyCode.LeftArrow)) {
            transform.position -= new Vector3(speed, 0, 0);
        }

        if (Input.GetKey(KeyCode.RightArrow)) {
            transform.position += new Vector3(speed, 0, 0);
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            if (!isFlying || nbJump < 1) {
                rigidBody.velocity = new Vector3(0, 5f, 0);
                isFlying = true;
                nbJump++;
            }
        }  

    }

    void OnCollisionEnter2D(Collision2D collisionInfo) {
        foreach (ContactPoint2D contact in collisionInfo.contacts) {
            if (contact.normal.y == 1f) {
                isFlying = false;
                nbJump = 0;
            }
        }
    }

    void OnCollisionExit2D(Collision2D collisionInfo) {
        isFlying = true;
    }





}
