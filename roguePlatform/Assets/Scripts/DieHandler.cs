using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DieHandler : MonoBehaviour {

    public GameObject DieFX;
    public AudioSource DieAudio;

    internal virtual void Die() {
        
        if (DieFX != null) {
            GameObject go = Instantiate(DieFX , transform.position , transform.rotation) as GameObject;
            
            Destroy(go , 1f);
        }
        Destroy(gameObject);
    }
}