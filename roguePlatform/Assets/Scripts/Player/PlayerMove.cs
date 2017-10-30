using UnityEngine;

namespace RoguePlateformer {

    public class PlayerMove : MonoBehaviour {
        [SerializeField]
        private float runSpeed = 10f;
        [SerializeField]
        private float jumpSpeed = 350f;
        [SerializeField]
        private float groundRadius;// 0.2f;
        [SerializeField]
        private float jumpNdVelocity = -7f;
        [SerializeField]
        private float dashSpeed = 5f;

        private float _currentDashingSpeed = 0f;
        private float _dashTimer = 0;

        private Rigidbody2D rigidBody;
        private Animator anim;
        private RaycastHit hit;

        private bool isFlying;
        //Variable which countains what direction the character is looking
        public bool facingRight;

        private int nbJump;


        //Variable for checking if in contact with ground
        public Transform groundCheck;
        public LayerMask whatIsGround;
        public Vector2 DashForceTestTweak;

        [Header("Used in Translate")]
        public AnimationCurve dashSpeedAnim;
        public float animDashDuration;





        public void Start() {
            rigidBody = GetComponent<Rigidbody2D>();
            isFlying = false;
            facingRight = true;
            nbJump = 0;
            anim = GetComponent<Animator>();
        }

        public void Update() {
            Jump();
        }

        public void FixedUpdate() {
            Dash();

            //Check if is on the ground 
            isFlying = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
            anim.SetBool("Ground", isFlying);

            //Jump Animation ... if is not falling go to Idle 
            anim.SetFloat("vSpeed", rigidBody.velocity.y);

            //Velocity and animation
            float move = Input.GetAxis("Horizontal");
            rigidBody.velocity = new Vector2(move * runSpeed, rigidBody.velocity.y);

            anim.SetFloat("Speed", Mathf.Abs(move));

            //Change Face Direction 
            if (move > 0 && !facingRight) { Flip(); } else if (move < 0 && facingRight) { Flip(); }
        }

        /// <summary>
        /// Dash following a curve with a raycast to detect obstacles
        /// </summary>
        public void Dash() {
            if (_currentDashingSpeed == 0f) {
                if (Input.GetButtonDown("Dash")) {
                    _currentDashingSpeed = facingRight ? dashSpeed : -dashSpeed;
                }
            }

            if (_currentDashingSpeed != 0f) {
                if (_dashTimer <= animDashDuration) {
                    anim.SetBool("Dash", true);
                    _dashTimer += Time.fixedDeltaTime;

                    float currentSpeed = _currentDashingSpeed * dashSpeedAnim.Evaluate(_dashTimer);
                    float dist = currentSpeed * Time.deltaTime;

                    Vector3 RaycastDirection = transform.right;
                    LayerMask MaskRayCast = new LayerMask();

                    if (!facingRight) {
                        RaycastDirection *= -1;
                    }

                    if (Physics.Raycast(transform.position + new Vector3(0, 0.5f, 0), RaycastDirection, out hit, dist, MaskRayCast)) {
                        _dashTimer = animDashDuration + 1;
                    } else {
                        transform.position = transform.position + new Vector3(dist, 0, 0);
                    }
                }

                if (_dashTimer > animDashDuration) {
                    _currentDashingSpeed = 0f;
                    _dashTimer = 0f;
                    anim.SetBool("Dash", false);
                }
            }
        }

        /// <summary>
        /// Jump and double jump
        /// </summary>
        public void Jump() {
            //Jump 
            if (isFlying && Input.GetButtonDown("Jump")) {
                anim.SetBool("Ground", false);
                ResetYVelocity();
                rigidBody.AddForce(new Vector2(0, jumpSpeed));
            }
            // Double Jump
            else if (!isFlying && Input.GetButtonDown("Jump") && nbJump < 1 && rigidBody.velocity.y > jumpNdVelocity) {
                rigidBody.AddForce(new Vector2(0, jumpSpeed));
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
        /// Reset player's rigidbody velocity in x
        /// </summary>
        private void ResetYVelocity() {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0);
        }

        /// <summary>
        /// Reset player's rigidbody velocity in y
        /// </summary>
        private void ResetXVelocity() {
            rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
        }

        public float GetRunSpeed() { return runSpeed; }

        public void SetRunSpeed(float speedP) { runSpeed = speedP; }

        public float GetJumpSpeed() { return jumpSpeed; }

        public void SetJumpSpeed(float speedJumpP) { jumpSpeed = speedJumpP; }

        public float GetDashSpeed() { return dashSpeed; }

        public void SetDashSpeed(float dashSpeedP) { dashSpeed = dashSpeedP; }
    }
}
