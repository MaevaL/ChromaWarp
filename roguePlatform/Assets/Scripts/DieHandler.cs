using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DieHandler : MonoBehaviour {

    public GameObject DieFX;
    public AudioClip DieAudio;

    internal virtual void Die() {
        if (DieFX != null) {
            GameObject go = Instantiate(DieFX, transform.position, transform.rotation) as GameObject;
            SoundManager.instance.PlaySingle(DieAudio);
            Destroy(go, 1f);
        }

        Destroy(gameObject);
    }
}