using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : Projectile {

    public GameObject ImpactFX;
    public GameObject NoImpactFX;
    

    protected override void SpecificCollision(LifeController lifeController, Collision2D col) {
        if(lifeController != null && lifeController.CompareTag("Enemy")) {
            ColorController EnemyColor = col.collider.gameObject.GetComponent<ColorController>();

            if (EnemyColor.SameColor(color)) {
                GameObject go = Instantiate(ImpactFX , transform.position , transform.rotation) as GameObject;
                lifeController.LoseLife(DamagesToEnemy);
            }
            else {
                GameObject go = Instantiate(NoImpactFX , col.transform.position , transform.rotation) as GameObject;
            }
            
        }
    }
}