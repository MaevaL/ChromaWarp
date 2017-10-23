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

            JumpCheck();

            DashCheck();

        }

        /// <summary>
        /// Lance une coroutine qui lance l'action du dash avec une certaine vitesse
        /// </summary>
        public void DashCheck() {
            // dash 
            if (Input.GetButtonDown("Dash")) {
                if (facingRight) {
                    StartCoroutine(DashTranslate(speedDash));
                }

                if (!facingRight) {
                    StartCoroutine(DashTranslate(-speedDash));
                }
            }
        }

        /// <summary>
        /// Utilsie le curve, mais ne marche pas car set a chaque frame le velocity bin .... Il CASSE LES COUILLES
        /// </summary>
        /// <param name="multiply"></param>
        /// <returns></returns>
        //IEnumerator Dash(float multiply) {
        //    float timer = 0;
        //    dashSpeed.preWrapMode = WrapMode.Loop;
        //    dashSpeed.postWrapMode = WrapMode.Loop;

        //    while (timer < animDashDuration) {
        //        rigidBody.velocity = new Vector2(multiply * dashSpeed.Evaluate(timer), rigidBody.velocity.y);
        //        Debug.Log(rigidBody.velocity);
        //        timer += Time.deltaTime;
        //        yield return new WaitForEndOfFrame();
        //    }
        //}

        /// <summary>
        /// Calcule toutes les positions qui vont être utilisé dans l'action du Dash via une courbe
        /// jusqu'à la fin de la frame.
        /// </summary>
        /// <param name="multiply">Permet de gerer la vitesse de l'action</param>
        /// <returns></returns>
        IEnumerator DashTranslate(float multiply) {
            float timer = 0;
            dashSpeed.preWrapMode = WrapMode.PingPong;
            dashSpeed.postWrapMode = WrapMode.PingPong;

            while (timer < animDashDuration) {
                transform.position += new Vector3(multiply * dashSpeed.Evaluate(timer) * Time.deltaTime, rigidBody.velocity.y, 0);
                Debug.Log("Test Dash Update Pos : " + transform.position);
                timer += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
        }

        /// <summary>
        /// Méthode gérant le saut et le double saut
        /// </summary>
        public void JumpCheck() {
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
