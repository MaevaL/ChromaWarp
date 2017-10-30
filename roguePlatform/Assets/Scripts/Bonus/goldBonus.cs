using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldBonus : MonoBehaviour {

    private GameObject player;
    public AudioClip soundGold;

    private void Awake() {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    public void SetBonus(int arg) {
        player.GetComponent<GoldController>().GainEnergy(arg);
    }

    private void OnCollisionEnter2D(Collision2D col) {
        if (col.collider.CompareTag("Player")) {
            SetBonus(1);
            SoundManager.instance.PlaySingle(soundGold);
            Destroy(gameObject);
        }
    }
}
