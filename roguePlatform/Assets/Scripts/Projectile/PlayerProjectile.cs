using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : Projectile {

    protected override void SpecificCollision(LifeController lifeController) {
        if(lifeController != null && lifeController.CompareTag("Enemy")) {
            lifeController.LoseLife(Damages);
        }
    }
}