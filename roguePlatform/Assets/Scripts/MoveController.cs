using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour {

    [SerializeField]
    private float speed;
    [SerializeField]
    private float speedJump;
    [SerializeField]
    private float groundRadius;// 0.2f;

    private Rigidbody2D rigidBody;

    private bool isFlying;
    private bool facingRight;

    private int nbJump;

    private Animator anim;

    public Transform groundCheck;
    public LayerMask whatIsGround;
    private Vector3 _prevPos;


    public void Start()
    {
        speed = 2f;
        rigidBody = GetComponent<Rigidbody2D>();
        isFlying = false;
        facingRight = true;
        nbJump = 0;
        anim = GetComponent<Animator>();
    }

    public void FixedUpdate()
    {

        //Check if is on the ground 
        isFlying = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        anim.SetBool("Ground", isFlying);

        //Jump Animation 
        anim.SetFloat("vSpeed", rigidBody.velocity.y);

        //Adjust the velocity
        float move = -1;

        rigidBody.velocity = new Vector2(move * speed, rigidBody.velocity.y);
        anim.SetFloat("Speed", Mathf.Abs(move));

        //Change Face Direction 
        if (move > 0 && !facingRight) { Flip(); }
        else if (move < 0 && facingRight) { Flip(); }

        JumpCheck();

        // dash 
        if (Input.GetButtonDown("Dash"))
        {
            float dash = Input.GetAxis("Dash");
            rigidBody.AddForce(new Vector2(speed * 100, 0));
        }

    }

    private void Update()
    {




    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
