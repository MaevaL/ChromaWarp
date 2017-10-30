using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDieHandler : DieHandler {

    public GameObject healthBonus;
    public GameObject goldBonus;

    /// <summary>
    /// Appelle le comportement de la méthode virtuelle Die() de la classe DieHandler
    /// en ajoutant l'instantiation de bonus à la mort de l'ennemi
    /// </summary>
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