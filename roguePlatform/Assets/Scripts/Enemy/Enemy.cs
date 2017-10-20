using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    
    LifeController lifeController;
    ColorController colorController; 
    MoveController moveController;
    public GameObject explosion;
    public GameObject healthBonus;
    public GameObject goldBonus;

    void Start() {
        colorController = gameObject.GetComponent<ColorController>();
        if (colorController.GetColor() == 1) {
            this.GetComponent<SpriteRenderer>().color = new Color(0f , 0f , 1f , 1f);
        }
        else {
            this.GetComponent<SpriteRenderer>().color = new Color(1f , 0f , 0f , 1f);
        }
        lifeController = gameObject.GetComponent<LifeController>(); 

    }

    private void OnTriggerEnter2D(Collider2D collider) {
        //TODO : ajuster le tag après l'implémentation des projectiles)
        if (collider.CompareTag("Projectile")) {
            lifeController.LoseLife(1); 
            if (lifeController.GetLife() <= 0)
            {
                Instantiate(explosion, transform.position, Quaternion.identity);
                Destroy(gameObject);

                float rnd = UnityEngine.Random.Range(0, 10);

                if (rnd == 1)
                {
                    Instantiate(healthBonus, transform.position + new Vector3(1, 0, 0), Quaternion.identity);
                }
                else
                {
                    Instantiate(goldBonus, transform.position + new Vector3(3, 0, 0), Quaternion.identity);
                }
            }   
        }
    }
}