using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTriggerController : MonoBehaviour {

    public int Damage = 1;

    void Start() {
        
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (!col.isTrigger &&  col.CompareTag("Enemy")) {
            SpecificCollision(col.gameObject.GetComponent<LifeController>(), col);
        }
        
        return;
    }




    public GameObject ImpactFX;
    public GameObject NoImpactFX;

    protected void SpecificCollision(LifeController lifeController, Collider2D col) {

        if (lifeController != null && lifeController.CompareTag("Enemy")) {
            ColorController EnemyColor = col.gameObject.GetComponent<ColorController>();
            ColorController PlayerColor = GameObject.FindWithTag("Player").GetComponent<ColorController>();
            if (PlayerColor.SameColor(EnemyColor.GetColor())) {
                GameObject go = Instantiate(ImpactFX, col.transform.position, transform.rotation) as GameObject;
                lifeController.LoseLife(Damage);
            }
            else {
                GameObject go = Instantiate(NoImpactFX, col.transform.position, transform.rotation) as GameObject;
            }

        }
    }

}
