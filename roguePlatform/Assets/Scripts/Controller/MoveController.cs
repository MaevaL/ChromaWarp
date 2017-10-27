using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour {

    [SerializeField]
    private float speed = 2f;
    [SerializeField]
    private float groundRadius;// 0.2f;
    [SerializeField]
    private bool facingRight = true;
    [SerializeField]
    public int type = 0;
    private Rigidbody2D rigidBody;

    private bool isFlying;

    private int nbJump;

    private Animator anim;

    public Transform groundCheck;
    public LayerMask whatIsGround;
    private Vector3 _prevPos;

    private Transform transfMove;
    float widthEnemy;

    public Transform target;
    float rotationSpeed = 3;
    float rangeMin = 1f;
    [SerializeField]
    public float rangeMax = 10f;
    float stop = 0f;
    float distance;
    public bool isClose = false;

    float hauteurMax;
    float hauteurMin;
    float rayonPatrol = 3f;

    //Turret
    public bool lookingRight;



    public void Start() {
        rigidBody = GetComponent<Rigidbody2D>();
        isFlying = false;
        anim = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;
        transfMove = this.transform;
        widthEnemy = this.GetComponent<SpriteRenderer>().bounds.extents.x;
        hauteurMax = transfMove.position.y + rayonPatrol;
        hauteurMin = transfMove.position.y - rayonPatrol;

    }

    public void FixedUpdate() {


        //Check if is on the ground 
        isFlying = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        anim.SetBool("Ground", isFlying);

        //Velocity Animation ... Idle, Walking, Running
        anim.SetFloat("vSpeed", rigidBody.velocity.y);

        switch (type) {
            case 0:
                MoveOnPlatform();
                break;

            case 1:
                MoveEndlessFront();
                break;

            case 2:
                MoveFollowPlayer();
                break;

            case 3:
                MoveFlying();
                break;

            case 4:
                MoveTurret();
                break;

            default:
                MoveEndlessFront();
                break;

        }


    }

    //Flip animation 
    private void Flip() {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void OnTriggerStay2D(Collider2D collider) {
        if (collider.CompareTag("Enemy")) {
            Vector3 currRotate = transfMove.eulerAngles;
            currRotate.y += 180;
            transfMove.eulerAngles = currRotate;
        }
    }

    void OnCollisionEnter2D(Collision2D collider) {
        if (collider.gameObject.CompareTag("Enemy")) {
            return;
        }
    }

    void OnCollisionStay2D(Collision2D collider) {
        if (collider.gameObject.CompareTag("Enemy")) {
            return;
        }
    }


    void MoveEndlessFront() {
        //Adjust the velocity
        float move = -1;
        rigidBody.velocity = new Vector2(move * speed, rigidBody.velocity.y);
        anim.SetFloat("Speed", Mathf.Abs(move));

        if (move > 0 && !facingRight) { Flip(); } else if (move < 0 && facingRight) { Flip(); }
    }

    void MoveOnPlatform() {
        Vector2 lineCastPos = transfMove.position - transfMove.right * widthEnemy;
        bool isGrounded = Physics2D.Linecast(lineCastPos, lineCastPos - Vector2.up, whatIsGround);
        Debug.DrawLine(lineCastPos, lineCastPos + Vector2.down);
        Debug.DrawRay(lineCastPos, lineCastPos + Vector2.down);
        Vector2 transfMoveRight = transfMove.right * 0.02f;
        // bool isBlocked = Physics2D.Linecast(lineCastPos, lineCastPos - transfMoveRight, whatIsGround);

        if (isGrounded) {
            Vector3 currRotate = transfMove.eulerAngles;
            currRotate.y += 180;
            transfMove.eulerAngles = currRotate;
        }


        rigidBody.velocity = new Vector2(transfMove.right.x * speed, rigidBody.velocity.y);

    }

    void MoveFollowPlayer() {
        Vector3 displacement = target.position - transform.position;
        displacement = displacement.normalized;
        if (Vector2.Distance(target.position, transform.position) > rangeMin && Vector2.Distance(target.position, transform.position) < rangeMax) {
            anim.SetTrigger("RunT");
            if (displacement.x > 0 && !facingRight) { Flip(); } else if (displacement.x < 0 && facingRight) { Flip(); }
            transform.position += (displacement * speed * Time.deltaTime);
            isClose = false;

        }
        else {
            //do whatever the enemy has to do with the player
            isClose = true;
        }
    }

    void MoveFlying() {
        //this.transform.position.Set(this.transform.position.x, (this.transform.position.y + 1), this.transform.position.z);

        if (transform.position.y >= hauteurMax) {
            speed = -speed;
        }
        if (transform.position.y < hauteurMin) {
            speed = -speed;
        }

        this.transform.Translate(new Vector3(0, speed, 0) * Time.deltaTime);

    }

    void MoveTurret() {

        distance = Vector3.Distance(transform.position, target.transform.position);


        if(target.transform.position.x > transform.position.x && !facingRight) {
            lookingRight = true;
            Flip();
        }
        if (target.transform.position.x < transform.position.x && facingRight) {
            lookingRight = false;
            Flip();
        }


        //float move = -1;
        //rigidBody.velocity = new Vector2(move * speed, rigidBody.velocity.y);
        //anim.SetFloat("Speed", Mathf.Abs(move));

        //if (move > 0 && !facingRight) { Flip(); } else if (move < 0 && facingRight) { Flip(); }
    }
}
