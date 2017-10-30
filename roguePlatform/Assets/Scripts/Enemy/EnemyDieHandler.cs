using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  Enemy Die Handler
/// </summary>
public class EnemyDieHandler : DieHandler {

    public GameObject healthBonus;
    public GameObject goldBonus;

    // Call the virtual method Die() of DieHandler
    // and instanciate an objet for the player
    internal override void Die() {
        base.Die();

        float rnd = UnityEngine.Random.Range(0, 10);
        
        if (rnd == 1) {
            Instantiate(healthBonus, transform.position + new Vector3(1, 0, 0), Quaternion.identity);
        } else {
            Instantiate(goldBonus, transform.position + new Vector3(1.5f, 0, 0), Quaternion.identity);
        }
    }
}