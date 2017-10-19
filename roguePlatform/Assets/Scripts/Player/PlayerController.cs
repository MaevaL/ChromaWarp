using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class PlayerController : MonoBehaviour {

    LifeController lifeController;
    public Text healthText;
    GoldController goldController;
    public Text CoinText;

    // Use this for initialization
    void Start() {

        lifeController = gameObject.GetComponent<LifeController>();
        healthText = GameObject.Find("HealthText").GetComponent<Text>();
        healthText.text = ": "+ lifeController.GetLife();

        goldController = gameObject.GetComponent<GoldController>();
        CoinText = GameObject.Find("CoinText").GetComponent<Text>();
        CoinText.text = ": " + goldController.GetGold();

    }

    // Update is called once per frame
    void Update() {
        healthText.text = ": " + (lifeController.GetLife());
        CoinText.text = ": " + goldController.GetGold();
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider == null) { return; }
    }
}
