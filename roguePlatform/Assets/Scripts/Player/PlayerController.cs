using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class PlayerController : MonoBehaviour {

    LifeController lifeController;
    public Text healthText;
    public GameObject bonusHealth;

    // Use this for initialization
    void Start() {

        lifeController = gameObject.GetComponent<LifeController>();
        healthText = GameObject.Find("HealthText").GetComponent<Text>();
        healthText.text = ": "+ lifeController.GetLife();

    }

    // Update is called once per frame
    void Update() {
        healthText.text = ": " + (lifeController.GetLife());
    }

    void OnTriggerEnter2D(Collider2D collider) {
        /*
         * without that asteroid is destroyed at the very 1st frame
         * by boundary's which declench our triggerEnter
         */
        if (collider == null) { return; }

        if (collider.CompareTag("Enemy")) {
            Instantiate(bonusHealth , transform.position + new Vector3(1,0,0) , Quaternion.identity);
            Destroy(collider.gameObject);
        }

        if (collider.CompareTag("HealthBonus")) {
            collider.gameObject.GetComponent<Bonus>().SetBonus(1);
        }

        /*
         * Destroy gameobject's script attach and his children
         */
        
    }

}
