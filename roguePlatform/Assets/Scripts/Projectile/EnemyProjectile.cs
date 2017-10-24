using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : Projectile {

    public GameObject ImpactFX;

    protected override void SpecificCollision(LifeController lifeController, Collision2D col) {
        if (lifeController != null && lifeController.CompareTag("Player")) {
            GameObject go = Instantiate(ImpactFX , col.collider.transform.position , transform.rotation) as GameObject;
            lifeController.LoseLife(Damages);
        }
    }
}
