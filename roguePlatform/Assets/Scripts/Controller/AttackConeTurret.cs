using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  Attack Cone Turret controller
///  Manage the collider of the distant attack of the turret Enemy
/// </summary>
public class AttackConeTurret : MonoBehaviour {

    private FireEnemy fireEnemy;
    private float timerFireRate;

    public bool isLeft = false;

    void Awake() {
        fireEnemy = gameObject.GetComponentInParent<FireEnemy>();
    }

    void Start() {
        timerFireRate = fireEnemy.fireRate;
    }

    //Check if the Player enter in the view field of the turret
    void OnTriggerStay2D(Collider2D col) {
        timerFireRate -= (float)0.01;

        if (col.CompareTag("Player")) {
            if (timerFireRate <= 0) {
                timerFireRate = fireEnemy.fireRate;
                //If the Player is on the left of the turret
                if (isLeft) {
                    fireEnemy.TurretFire(true);
                }
                else {
                    fireEnemy.TurretFire(false);
                }
            }
        }
    }
}
