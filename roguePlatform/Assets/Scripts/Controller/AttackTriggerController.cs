using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  Attack Trigger controller
///  Manage the collider of the melee attack for Player & Enemy
/// </summary>
public class AttackTriggerController : MonoBehaviour {

    public int Damage;
    public GameObject ImpactFX;
    public GameObject NoImpactFX;

    void OnTriggerEnter2D(Collider2D col) {
        //When the Player attacks
        if (gameObject.GetComponentInParent<LifeController>().CompareTag("Player")) {
            //If the attack hit an Enemy
            if (!col.isTrigger && col.CompareTag("Enemy")) {
                SpecificCollision(col.gameObject.GetComponent<LifeController>(), col);
            }
        }
        //When an Enemy attacks
        else {
            //If the attack hit the Player
            if (!col.isTrigger && col.CompareTag("Player")) {
                SpecificCollision(col.gameObject.GetComponent<LifeController>(), col);
            }
        }
        return;
    }

    //Check the collision between the melee collider and an object
    protected void SpecificCollision(LifeController lifeController, Collider2D col) {
        //When the Player attacks
        if (gameObject.GetComponentInParent<LifeController>().CompareTag("Player")) {
            Damage = GameObject.FindWithTag("Player").GetComponent<PlayerController>().GetDamageMelee();
            //If Player attacks an Enemy with melee attack
            if (lifeController != null && lifeController.CompareTag("Enemy")) {
                ColorController EnemyColor = col.gameObject.GetComponent<ColorController>();
                ColorController PlayerColor = GameObject.FindWithTag("Player").GetComponent<ColorController>();
                //If the color is the same between Player & Enemy, melee attack works
                if (PlayerColor.SameColor(EnemyColor.GetColor())) {
                    GameObject go = Instantiate(ImpactFX, col.transform.position, transform.rotation) as GameObject;
                    lifeController.LoseLife(Damage);
                }
                else {
                    GameObject go = Instantiate(NoImpactFX, col.transform.position, transform.rotation) as GameObject;
                }
            }
        }
        //When an Enemy attacks
        else {
            Damage = gameObject.GetComponentInParent<Enemy>().damageMelee;
            //If Enemy attacks the Player with melee attack
            if (lifeController != null && lifeController.CompareTag("Player")) {
                ColorController EnemyColor = gameObject.GetComponentInParent<ColorController>();
                ColorController PlayerColor = GameObject.FindWithTag("Player").GetComponent<ColorController>();
                //If the color is the same between Enemy & Player, melee attack works
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

}
