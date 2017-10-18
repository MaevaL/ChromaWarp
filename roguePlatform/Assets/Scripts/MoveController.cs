﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour {

    [SerializeField]
    private float speed =2f;
    [SerializeField]
    private float groundRadius;// 0.2f;
    [SerializeField]
    private bool facingRight = true; 
    private Rigidbody2D rigidBody;

    private bool isFlying;
    

    private int nbJump;

    private Animator anim;

    public Transform groundCheck;
    public LayerMask whatIsGround;
    private Vector3 _prevPos;


    public void Start()
    { 
        rigidBody = GetComponent<Rigidbody2D>();
        isFlying = false;
        anim = GetComponent<Animator>();
    }

    public void FixedUpdate()
    {
        //Check if is on the ground 
        isFlying = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        anim.SetBool("Ground", isFlying);

        //Velocity Animation ... Idle, Walking, Running
        anim.SetFloat("vSpeed", rigidBody.velocity.y);

        //Adjust the velocity
        float move = -1;
        rigidBody.velocity = new Vector2(move * speed, rigidBody.velocity.y);
        anim.SetFloat("Speed", Mathf.Abs(move));

        //Change Face Direction 
        Debug.Log(move); 
        if (move > 0 && !facingRight) { Flip(); }
        else if (move < 0 && facingRight) { Flip(); }
    }

    //Flip animation 
    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
