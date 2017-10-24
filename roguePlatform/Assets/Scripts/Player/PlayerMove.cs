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
        private float speedDash = 5f;
        private float _currentDashingSpeed = 0f;
        private float _dashTimer = 0;

        private Rigidbody2D rigidBody;

        private bool isFlying;
        //Variable which countains what direction the character is looking
        public bool facingRight;

        private int nbJump;

        private Animator anim;
        //Variable for checking if in contact with ground
        public Transform groundCheck;
        public LayerMask whatIsGround;

        private Vector3 _prevPos;

        public Vector2 DashForceTestTweak;

        [Header("Used in Translate")]
        public AnimationCurve dashSpeed;
        public float animDashDuration;



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
            if (move > 0 && !facingRight) { Flip(); } else if (move < 0 && facingRight) { Flip(); }

            Jump();

            Dash();

        }

        /// <summary>
        /// Lance une coroutine qui lance l'action du dash avec une certaine vitesse
        /// </summary>
        public void Dash() {
            // dash
            if (_currentDashingSpeed == 0f) {
                if (Input.GetButtonDown("Dash")) {
                    _currentDashingSpeed = facingRight ? speedDash : -speedDash;
                }
            }

            if ( _currentDashingSpeed != 0f ) {
                if (_dashTimer <= animDashDuration) {
                    anim.SetBool("Dash" , true);
                    _dashTimer += Time.fixedDeltaTime;
                    transform.position = transform.position + new Vector3(_currentDashingSpeed * dashSpeed.Evaluate(_dashTimer) * Time.fixedDeltaTime , 0 , 0);
                }

                if (_dashTimer > animDashDuration) {
                    _currentDashingSpeed = 0f;
                    _dashTimer = 0f;
                    anim.SetBool("Dash" , false);
                }
            }
        }
        
        /// <summary>
        /// Méthode gérant le saut et le double saut
        /// </summary>
        public void Jump() {
            //Jump 
            if (isFlying && Input.GetButtonDown("Jump")) {
                anim.SetBool("Ground", false);
                ResetYVelocity();
                rigidBody.AddForce(new Vector2(0, speedJump));
            }
            // Double Jump
            else if (!isFlying && Input.GetButtonDown("Jump") && nbJump < 1 && rigidBody.velocity.y > jumpNdVelocity) {
                rigidBody.AddForce(new Vector2(0, speedJump));
                ResetYVelocity();
                nbJump++;
            }
            // Reinitialize Jump
            if (isFlying == true) {
                nbJump = 0;
            }
        }

        /// <summary>
        /// Permet de faire se retourner le sprite du player lors d'un changement de sens
        /// </summary>
        private void Flip() {
            facingRight = !facingRight;

            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }

        /// <summary>
        /// Reset la velocité associé au rigidbody du player en x
        /// </summary>
        private void ResetYVelocity() {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0);
        }

        /// <summary>
        /// Reset la velocité associé au rigidbody du player en y
        /// </summary>
        private void ResetXVelocity() {
            rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
        }

    }
}
