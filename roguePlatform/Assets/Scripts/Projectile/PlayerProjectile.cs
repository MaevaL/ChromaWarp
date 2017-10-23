using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : Projectile {

    public GameObject ImpactFX;
    protected override void SpecificCollision(LifeController lifeController, Collision2D col) {
        if(lifeController != null && lifeController.CompareTag("Enemy")) {
            GameObject go = Instantiate(ImpactFX , transform.position, transform.rotation) as GameObject;
            lifeController.LoseLife(Damages);
        }
    }
}