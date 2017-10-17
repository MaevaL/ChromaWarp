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
        private Animator anim;
        private bool facingRight;
        public Transform groundCheck;
        private float groundRadius = 0.2f;
        public LayerMask whatIsGround; 

        public void Start() {
            speed = 10f;
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

            //Jump Animation 
            anim.SetFloat("vSpeed", rigidBody.velocity.y); 

            //Adjust the velocity
            float move = Input.GetAxis("Horizontal");
            rigidBody.velocity = new Vector2(move * speed, rigidBody.velocity.y);
            anim.SetFloat("Speed", Mathf.Abs(move));
            //Change Face Direction 
            if (move > 0 && !facingRight)
            {
                Flip();
            }
            else if (move < 0 && facingRight)
            {
                Flip();
            }
            
        }
        
        private void Update()
        {
            //Jump 
            if (isFlying && Input.GetButtonDown("Jump"))
            {
                anim.SetBool("Ground", false);
                rigidBody.AddForce(new Vector2(0, speedJump)); 
            }
            // Double Jump
            else if (!isFlying && Input.GetButtonDown("Jump") && nbJump<1)
            {
                rigidBody.AddForce(new Vector2(0, speedJump));
                nbJump++; 
            }
            // Reinitialize Jump
            if (isFlying == true)
            {
                nbJump = 0; 
            }
        }

        private void Flip()
        {
            facingRight = !facingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
        
    }
}
