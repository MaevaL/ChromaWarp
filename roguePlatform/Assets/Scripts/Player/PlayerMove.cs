using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoguePlateformer {
    public class PlayerMove : MonoBehaviour {
        [SerializeField]
        private float speed = 10f;
        [SerializeField]
        private float speedJump = 350f;
        [SerializeField]
        private float groundRadius;// 0.2f;
        [SerializeField]
        private float jumpNdVelocity = -7f;
        [SerializeField]
        private float speedDash = 5000f;
        
        private Rigidbody2D rigidBody;

        private bool isFlying;
        //Variable which countains what direction the character is looking
        private bool facingRight;

        private int nbJump;

        private Animator anim;
        //Variable for checking if in contact with ground
        public Transform groundCheck;
        public LayerMask whatIsGround;
        private Vector3 _prevPos;
       

        public void Start() {
            rigidBody = GetComponent<Rigidbody2D>();
            isFlying = false;
            facingRight = true; 
            nbJump = 0;
            anim = GetComponent<Animator>(); 
        }

        public void FixedUpdate() {
            
            //Check if is on the ground 
            isFlying = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
            anim.SetBool("Ground", isFlying);

            //Jump Animation ... if is not falling go to Idle 
            anim.SetFloat("vSpeed", rigidBody.velocity.y); 

            //Velocity and animation
            float move = Input.GetAxis("Horizontal");
            rigidBody.velocity = new Vector2(move * speed, rigidBody.velocity.y);
            anim.SetFloat("Speed", Mathf.Abs(move));
            
            //Change Face Direction 
            if (move > 0 && !facingRight) { Flip(); }
            else if (move < 0 && facingRight) { Flip(); }

            JumpCheck();

            dashCheck(); 

        }
        
        public void dashCheck()
        {
            // dash 
            if (Input.GetButtonDown("Dash"))
            {
                float dash = Input.GetAxis("Dash");
                ResetXVelocity();
                if (facingRight)
                { 
                    rigidBody.AddForce(new Vector2(speedDash, 0));
                }
                if (!facingRight)
                {
                    rigidBody.AddForce(new Vector2(-speedDash, 0)); 
                }
               
            }
        }

        public void JumpCheck() {
            //Jump 
            if (isFlying && Input.GetButtonDown("Jump")) {
                anim.SetBool("Ground" , false);
                ResetYVelocity(); 
                rigidBody.AddForce(new Vector2(0 , speedJump));
            }
            // Double Jump
            else if (!isFlying && Input.GetButtonDown("Jump") && nbJump < 1 && rigidBody.velocity.y > jumpNdVelocity) {
                rigidBody.AddForce(new Vector2(0 , speedJump));
                ResetYVelocity();
                nbJump++;
            }
            // Reinitialize Jump
            if (isFlying == true) {
                nbJump = 0;
            }
        }

        private void Flip()  {
            facingRight = !facingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }

        private void ResetYVelocity()
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0); 
        }

        private void ResetXVelocity()
        {
            rigidBody.velocity = new Vector2(0, rigidBody.velocity.y); 
        }

    }
}
