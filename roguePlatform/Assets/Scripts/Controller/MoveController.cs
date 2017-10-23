using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{

    [SerializeField]
    private float speed = 2f;
    [SerializeField]
    private float groundRadius;// 0.2f;
    [SerializeField]
    private bool facingRight = true;
    [SerializeField]
    private float type = 0;
    private Rigidbody2D rigidBody;

    private bool isFlying;

    private int nbJump;

    private Animator anim;

    public Transform groundCheck;
    public LayerMask whatIsGround;
    private Vector3 _prevPos;

    private Transform transfMove;
    float widthEnemy;

    public Transform target; //cant't be bothered to do any commments
    float moveSpeed = 5f;
    float rotationSpeed = 3;
    float rangeMin = 1f;
    float rangeMax = 10f;
    float stop = 0f;
    public Transform myTransform;
    float distance;


    public void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        isFlying = false;
        anim = GetComponent<Animator>();
        myTransform = this.transform;
        target = GameObject.FindWithTag("Player").transform;
        transfMove = this.transform;
        widthEnemy = this.GetComponent<SpriteRenderer>().bounds.extents.x;
    }

    public void FixedUpdate()
    {


        //Check if is on the ground 
        isFlying = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        anim.SetBool("Ground", isFlying);

        //Velocity Animation ... Idle, Walking, Running
        anim.SetFloat("vSpeed", rigidBody.velocity.y);

        if (type == 0)
        {
            Vector2 lineCastPos = transfMove.position - transfMove.right * widthEnemy;
            bool isGrounded = Physics2D.Linecast(lineCastPos, lineCastPos - Vector2.up, whatIsGround);
            Debug.DrawLine(lineCastPos, lineCastPos + Vector2.down);
            Debug.DrawRay(lineCastPos, lineCastPos + Vector2.down);
            Vector2 transfMoveRight = transfMove.right * 0.02f;
            bool isBlocked = Physics2D.Linecast(lineCastPos, lineCastPos - transfMoveRight, whatIsGround);

            if (isGrounded)
            {
                Vector3 currRotate = transfMove.eulerAngles;
                currRotate.y += 180;
                transfMove.eulerAngles = currRotate;
            }


            rigidBody.velocity = new Vector2(transfMove.right.x * speed, rigidBody.velocity.y);


        }
        else if (type == 2)
        {

            Vector3 displacement = target.position - transform.position;
            displacement = displacement.normalized;
            if (Vector2.Distance(target.position, transform.position) > rangeMin && Vector2.Distance(target.position, transform.position) < rangeMax)
            {
                if (displacement.x > 0 && !facingRight) { Flip(); } else if (displacement.x < 0 && facingRight) { Flip(); }
                transform.position += (displacement * moveSpeed * Time.deltaTime);

            }
            else
            {
                //do whatever the enemy has to do with the player
            }



        }
        else
        {
            //Adjust the velocity
            float move = -1;
            rigidBody.velocity = new Vector2(move * speed, rigidBody.velocity.y);
            anim.SetFloat("Speed", Mathf.Abs(move));

            //Change Face Direction 
            //Debug.Log(move); 

            if (move > 0 && !facingRight) { Flip(); } else if (move < 0 && facingRight) { Flip(); }

        }



    }

    //Flip animation 
    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.CompareTag("Enemy"))
        {
            Vector3 currRotate = transfMove.eulerAngles;
            currRotate.y += 180;
            transfMove.eulerAngles = currRotate;
        }
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.CompareTag("Enemy"))
        {
            return;
        }
    }

    void OnCollisionStay2D(Collision2D collider)
    {
        if (collider.gameObject.CompareTag("Enemy"))
        {
            return;
        }
    }
}
