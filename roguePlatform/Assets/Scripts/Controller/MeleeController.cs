using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  Melee controller
///  Melee attack for Player and Enemy
/// </summary>
public class MeleeController : MonoBehaviour {

    [SerializeField]
    private float attackCooldown = 0.3f;
    [SerializeField]
    private float animShootDuration;

    private bool attacking = false;
    private float attackTimer = 0;
    private Animator anim;

    public Collider2D attackTrigger;

    void Start() {
        anim = GetComponent<Animator>();
        attackTrigger.enabled = false;
    }

    void Update() {
        MeleeAttack();
    }

    private void MeleeAttack() {
        //When the Player attacks
        if (gameObject.CompareTag("Player")) {
            //Player can only do a melee attack when his color is red(2)
            if (Input.GetButtonDown("Fire2") && !attacking && GetComponent<ColorController>().GetColor() == 2) {
                attacking = true;
                attackTimer = attackCooldown;
                attackTrigger.enabled = true;
                anim.SetTrigger("PunchT");
            }
            //cooldown melee attack
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
        //When Enemy attacks
            //Enemy attacks if it is close to the player
            if (gameObject.GetComponent<MoveController>().isClose && !attacking) {
                attacking = true;
                attackTimer = attackCooldown;
                attackTrigger.enabled = true;
                anim.SetTrigger("PunchT");
            }
            //cooldown melee attack
            if (attacking) {
                if (attackTimer > 0) {
                    attackTimer -= Time.deltaTime;
                    attackTrigger.enabled = false;
                }
                else {
                    attacking = false;
                    attackTrigger.enabled = true;
                }
            }
        }
    }

    public float GetAttackCooldown() {
        return attackCooldown;
    }

    public void SetAttackCoolDown(float cooldown) {
        if (cooldown >= 0) {
            attackCooldown = cooldown;
        }
    }
}
