using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldBonus : MonoBehaviour {

    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    public void SetBonus(int arg)
    {
        player.GetComponent<GoldController>().GainGold(arg);
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.CompareTag("Player")) {
            SetBonus(1);
            Destroy(gameObject);
        }
    }
}
