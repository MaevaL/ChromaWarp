using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoguePlateformer {
    public class PlayerMove : MonoBehaviour {
        [SerializeField]
        private float speed;
        [SerializeField]
        private float speedJump;
        private Rigidbody2D rigidBody;
        private bool isFlying;
        private int nbJump;


        public void Start() {
            speed = 4f;
            rigidBody = GetComponent<Rigidbody2D>();
            isFlying = false;
            nbJump = 0;

        }

        public void FixedUpdate() {
            float horizontalSpeed = 0;

            horizontalSpeed += speed * Input.GetAxisRaw("Horizontal");

            Vector2 newVelocity = rigidBody.velocity;

            if (Input.GetButtonDown("Jump")) {
                if (nbJump < 2) {
                    newVelocity.y = speedJump;
                    isFlying = true;
                    nbJump++;
                }
            }

            newVelocity.x = horizontalSpeed;
            rigidBody.velocity = newVelocity;

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
            if (nbJump == 0) {  nbJump = 1;   }
        }

    }
}
