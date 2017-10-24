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
        if (Input.GetButtonDown("Fire2") && !attacking) {
            attacking = true;
            attackTimer = attackCooldown;

            attackTrigger.enabled = true;

        }

        if (attacking) {
            if(attackTimer > 0) {
                attackTimer -= Time.deltaTime;
            }
            else {
                attacking = false;
                attackTrigger.enabled = false;
            }

        }
    }

    /// <summary>
    /// Délai entre deux tirs
    /// </summary>
    /// <returns></returns>
    IEnumerator RecoveryPunch(Vector3 hit) {

        anim.SetTrigger("ShootT");
        yield return new WaitForSeconds(attackCooldown / 2);

        attacking = true;
    }

}
