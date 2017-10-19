using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    
    LifeController lifeController;
    MoveController moveController;
    public GameObject explosion;
    public GameObject healthBonus;
    public GameObject goldBonus;

    private void OnTriggerEnter2D(Collider2D collider) {
        //TODO : ajuster le tag après l'implémentation des projectiles)
        if (collider.CompareTag("Player")) {
            Instantiate(explosion , transform.position , Quaternion.identity);
            Destroy(gameObject);

            float rnd = UnityEngine.Random.Range(0 , 2);

            if (rnd == 1) {
                Instantiate(healthBonus , transform.position + new Vector3(1 , 0 , 0) , Quaternion.identity);
            }
            else {
                Instantiate(goldBonus , transform.position + new Vector3(3 , 0 , 0) , Quaternion.identity);
            }
            
        }
    }
}
