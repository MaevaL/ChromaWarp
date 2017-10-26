using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackConeTurret : MonoBehaviour {

    private FireEnemy fireEnemy;

    public bool isLeft = false;
    private float timerFireRate;

    void Awake() {
        fireEnemy = gameObject.GetComponentInParent<FireEnemy>();
    }

	// Use this for initialization
	void Start () {
        timerFireRate = fireEnemy.fireRate;

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerStay2D(Collider2D col) {

        timerFireRate-=(float)0.01;

        if (col.CompareTag("Player")) {

            if (timerFireRate <= 0) {
                timerFireRate = fireEnemy.fireRate;
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
