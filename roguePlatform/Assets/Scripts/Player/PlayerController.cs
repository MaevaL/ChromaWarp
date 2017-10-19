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
    public GameObject healthBonus;
    public GameObject goldBonus; 

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
        /*
         * without that asteroid is destroyed at the very 1st frame
         * by boundary's which declench our triggerEnter
         */
        if (collider == null) { return; }
        //TODO : Resolve Collision Problem (spawn two element, detect two collision sometimes  ) 
        if (collider.CompareTag("Enemy")) {

            float rnd = UnityEngine.Random.Range(0, 2);
            
            Debug.Log(rnd); 
            if (rnd == 1) {
                Instantiate(healthBonus, transform.position + new Vector3(1, 0, 0), Quaternion.identity);
            }
            else {
                Instantiate(goldBonus, transform.position + new Vector3(3, 0, 0), Quaternion.identity);
            }
          
            Destroy(collider.gameObject);
        }

        if (collider.CompareTag("HealthBonus")) {

            collider.gameObject.GetComponent<HealthBonus>().SetBonus(1);
            Destroy(collider.gameObject);
            
        }

        if (collider.CompareTag("GoldBonus"))
        {

            collider.gameObject.GetComponent<GoldBonus>().SetBonus(1);
            Destroy(collider.gameObject);

        }

        /*
         * Destroy gameobject's script attach and his children
         */

    }

}
