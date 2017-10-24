using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : Projectile {

    public GameObject ImpactFX;
    public GameObject NoImpactFX;

    protected override void SpecificCollision(LifeController lifeController , Collision2D col) {
        if (lifeController != null && lifeController.CompareTag("Player")) {

            ColorController PlayerColor = col.collider.gameObject.GetComponent<ColorController>();
            ColorController EnemyColor = GameObject.FindWithTag("Enemy").GetComponent<ColorController>();

            if (PlayerColor.SameColor(EnemyColor.GetColor())) {
                GameObject go = Instantiate(ImpactFX , col.collider.transform.position + new Vector3(0.5f , 0.5f , 0) , transform.rotation) as GameObject;
                lifeController.LoseLife(Damages);
            }
            else {
                GameObject go = Instantiate(NoImpactFX , col.transform.position , transform.rotation) as GameObject;
            }
        }
    }



}
