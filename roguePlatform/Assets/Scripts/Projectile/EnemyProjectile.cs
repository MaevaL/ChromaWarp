using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : Projectile {

    protected override void SpecificCollision(LifeController lifeController) {
        if (lifeController != null && lifeController.CompareTag("Player")) {
            lifeController.LoseLife(Damages);
        }
    }
}
