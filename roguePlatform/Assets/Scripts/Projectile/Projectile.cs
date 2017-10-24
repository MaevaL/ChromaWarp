using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour {

    public int Damages = 1;
    public float LifeTime;
    void Start() {
            
        Destroy(gameObject , LifeTime);
    }

    private void OnCollisionEnter2D(Collision2D col) {

        if (!(col.collider.CompareTag("Ground")
            || col.collider.CompareTag("Environment"))) {

            SpecificCollision(col.collider.gameObject.GetComponent<LifeController>(), col);
        }

        Destroy(gameObject);
        return;
    }

    public void SetDamages(int param) {
        Damages = param;
    }

    protected abstract void SpecificCollision(LifeController lifeController, Collision2D col);
}