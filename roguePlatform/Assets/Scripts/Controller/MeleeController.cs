using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeController : MonoBehaviour {

    //Attack Melee
    private bool attacking = false;
    private float attackTimer = 0;
    [SerializeField]
    private float attackCooldown = 0.3f;
    public Collider2D attackTrigger;

    [SerializeField]
    private float animShootDuration;
    private Animator anim;


    // Use this for initialization
    void Start() {
        anim = GetComponent<Animator>();
        attackTrigger.enabled = false;
    }

    // Update is called once per frame
    void Update() {
        MeleeAttack();
    }


    private void MeleeAttack() {

        if (gameObject.CompareTag("Player")) {


            if (Input.GetButtonDown("Fire2") && !attacking && GetComponent<ColorController>().GetColor() == 2) {
                attacking = true;
                attackTimer = attackCooldown;

                attackTrigger.enabled = true;
                anim.SetTrigger("PunchT");

            }

            if (attacking) {
                if (attackTimer > 0) {
                    attackTimer -= Time.deltaTime;
                }
                else {
                    attacking = false;
                    attackTrigger.enabled = false;
                }

            }
        }
        else {

            if (!gameObject.GetComponent<MoveController>().isClose && !attacking) {
                attacking = true;
                attackTimer = attackCooldown;

                attackTrigger.enabled = true;
                anim.SetTrigger("PunchT");

            }

            if (attacking) {
                if (attackTimer > 0) {
                    attackTimer -= Time.deltaTime;
                }
                else {
                    attacking = false;
                    attackTrigger.enabled = false;
                }

            }

        }
    }

    public float GetAttackCooldown()
    {
        return attackCooldown; 
    }

    public void SetAttackCoolDown(float cooldown)
    {
        if(cooldown>=0)
        {
            attackCooldown = cooldown;
        } 
    }
}
