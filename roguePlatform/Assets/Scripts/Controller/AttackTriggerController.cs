using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTriggerController : MonoBehaviour {

    [SerializeField]
    private int damageToEnemy = 1;
    [SerializeField]
    private int damageToPlayer = 1;
    void Start() {
        
    }

    void OnTriggerEnter2D(Collider2D col) {

        //Debug.Log("objet: "+ gameObject.GetComponentInParent<LifeController>().tag);
        if (gameObject.GetComponentInParent<LifeController>().CompareTag("Player")) {


            if (!col.isTrigger && col.CompareTag("Enemy")) {
                SpecificCollision(col.gameObject.GetComponent<LifeController>(), col);
            }


        }
        else {
            if (!col.isTrigger && col.CompareTag("Player")) {
                SpecificCollision(col.gameObject.GetComponent<LifeController>(), col);
            }

        }
        return;
    }




    public GameObject ImpactFX;
    public GameObject NoImpactFX;

    protected void SpecificCollision(LifeController lifeController, Collider2D col) {

        if (gameObject.GetComponentInParent<LifeController>().CompareTag("Player")) {

            if (lifeController != null && lifeController.CompareTag("Enemy")) {
                ColorController EnemyColor = col.gameObject.GetComponent<ColorController>();
                ColorController PlayerColor = GameObject.FindWithTag("Player").GetComponent<ColorController>();
                if (PlayerColor.SameColor(EnemyColor.GetColor())) {
                    GameObject go = Instantiate(ImpactFX, col.transform.position, transform.rotation) as GameObject;
                    lifeController.LoseLife(damageToEnemy);
                }
                else {
                    GameObject go = Instantiate(NoImpactFX, col.transform.position, transform.rotation) as GameObject;
                }

            }
        }
        else {

            if (lifeController != null && lifeController.CompareTag("Player")) {
                ColorController EnemyColor = col.gameObject.GetComponent<ColorController>();
                ColorController PlayerColor = GameObject.FindWithTag("Player").GetComponent<ColorController>();
                if (PlayerColor.SameColor(EnemyColor.GetColor())) {
                    GameObject go = Instantiate(ImpactFX, col.transform.position, transform.rotation) as GameObject;
                    lifeController.LoseLife(damageToEnemy);
                }
                else {
                    GameObject go = Instantiate(NoImpactFX, col.transform.position, transform.rotation) as GameObject;
                }

            }

        }
    }

    public int GetDamageToEnemy()
    {
        return damageToEnemy; 
    }

    public int getDamageToPlayer()
    {
        return damageToPlayer;
    }

    public void SetDamageToEnemy(int damage)
    {
        damageToEnemy = damage; 
    }

    public void SetDamageToPlayer(int damage)
    {
        damageToPlayer = damage;
    }
}
