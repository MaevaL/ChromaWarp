using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnCollision : MonoBehaviour {

    private void OnCollisionEnter2D(Collision2D col) {
        if (col.collider.CompareTag("Ground") || col.collider.CompareTag("Environment") || col.collider.CompareTag("Enemy")) {
            Destroy(gameObject);
        }
    }
}