using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DieHandler : MonoBehaviour {

    public GameObject DieFX;

    internal virtual void Die() {
        if (DieFX != null) {
            GameObject go = Instantiate(DieFX , transform.position , transform.rotation) as GameObject;
            Destroy(go , 1f);
        }
        Destroy(gameObject);
    }
}