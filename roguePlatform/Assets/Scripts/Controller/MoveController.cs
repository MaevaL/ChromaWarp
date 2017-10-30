using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  Enemy Move controller
///  Different move patern
/// </summary>
public class MoveController : MonoBehaviour {

    [SerializeField]
    private float speed = 2f;
    [SerializeField]
    private float groundRadius;
    [SerializeField]
    private bool facingRight = true;
    [SerializeField]
    public float rangeMax = 10f;

    private Rigidbody2D rigidBody;
    private bool isFlying;
    private Animator anim;
    private Transform transfMove;
    private float widthEnemy;
    private float rangeMin = 1f;
    private float distance;
    private float hauteurMax;
    private float hauteurMin;
    private float rayonPatrol = 3f;

    public int type = 0;
    public Transform groundCheck;
    public LayerMask whatIsGround;
    public Transform target;
    public bool isClose = false;
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

        //Move Patern Assign
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

    //Enemy walk in a direction
    void MoveEndlessFront() {
        float move = -1;
        rigidBody.velocity = new Vector2(move * speed, rigidBody.velocity.y);
        anim.SetFloat("Speed", Mathf.Abs(move));

        if (move > 0 && !facingRight) { Flip(); } else if (move < 0 && facingRight) { Flip(); }
    }

    //Enemy walk on a platform
    void MoveOnPlatform() {
        Vector2 lineCastPos = transfMove.position - transfMove.right * widthEnemy;
        bool isGrounded = Physics2D.Linecast(lineCastPos, lineCastPos - Vector2.up, whatIsGround);
        Vector2 transfMoveRight = transfMove.right * 0.02f;

        if (isGrounded) {
            Vector3 currRotate = transfMove.eulerAngles;
            currRotate.y += 180;
            transfMove.eulerAngles = currRotate;
        }
        rigidBody.velocity = new Vector2(transfMove.right.x * speed, rigidBody.velocity.y);

    }

    //Enemy has a circle Range view and run after the player when he enter in it
    void MoveFollowPlayer() {
        Vector3 displacement = target.position - transform.position;
        displacement = displacement.normalized;
        //When Player is in the range view of the Enemy
        if (Vector2.Distance(target.position, transform.position) > rangeMin && Vector2.Distance(target.position, transform.position) < rangeMax) {
            anim.SetTrigger("RunT");
            if (displacement.x > 0 && !facingRight) { Flip(); } else if (displacement.x < 0 && facingRight) { Flip(); }
            transform.position += (displacement * speed * Time.deltaTime);
            isClose = true;
        }
        else {
            isClose = false;
        }
    }

    //Enemy with only vertical move and view to player position
    void MoveFlying() {
        distance = Vector3.Distance(transform.position, target.transform.position);

        //Flip to aim the Player
        if (target.transform.position.x > transform.position.x && facingRight) {
            Flip();
        }else if (target.transform.position.x < transform.position.x && !facingRight) {
            Flip();
        }

        //Move up or down when limit patrol reach
        if (transform.position.y >= hauteurMax) {
            speed = -speed;
        }
        if (transform.position.y < hauteurMin) {
            speed = -speed;
        }
        this.transform.Translate(new Vector3(0, speed, 0) * Time.deltaTime);

    }

    //Static enemy aim the player in view field 
    void MoveTurret() {
        distance = Vector3.Distance(transform.position, target.transform.position);

        if (target.transform.position.x > transform.position.x && !facingRight) {
            lookingRight = true;
            Flip();
        }
        if (target.transform.position.x < transform.position.x && facingRight) {
            lookingRight = false;
            Flip();
        }
    }

    public float GetSpeed() {
        return speed;
    }

    public void SetSpeed(int speedP) {
        speed = speedP;
    }


}
