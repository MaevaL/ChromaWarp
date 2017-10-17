using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class PlayerController : MonoBehaviour {

    LifeController lifeController;
    public Text healthText;

    // Use this for initialization
    void Start() {

        lifeController = gameObject.GetComponent<LifeController>();
        healthText = GameObject.Find("HealthText").GetComponent<Text>();
        healthText.text = ": "+ lifeController.GetLife();

    }

    // Update is called once per frame
    void Update() {
        if (Input.GetButtonDown("Jump")) {
            healthText.text = ": " + (lifeController.GetLife());
        }
    }

}
