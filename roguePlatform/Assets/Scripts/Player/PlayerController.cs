using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class PlayerController : MonoBehaviour
{

    LifeController lifeController;
    public Text healthText;
    GoldController goldController;
    public Text CoinText;

    public GameObject healthBonus;
    ColorController colorController;

    // Use this for initialization
    void Start()
    {

        lifeController = gameObject.GetComponent<LifeController>();
        healthText = GameObject.Find("HealthText").GetComponent<Text>();
        healthText.text = ": " + lifeController.GetLife();

        goldController = gameObject.GetComponent<GoldController>();
        CoinText = GameObject.Find("CoinText").GetComponent<Text>();
        CoinText.text = ": " + goldController.GetGold();

        colorController = gameObject.GetComponent<ColorController>();
        if(colorController.GetColor() == 1) {
            this.GetComponent<SpriteRenderer>().color = new Color(0.4f, 0.4f, 1f, 1f);
        }
        else {
            this.GetComponent<SpriteRenderer>().color = new Color(1f, 0.4f, 0.4f, 1f);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = ": " + (lifeController.GetLife());
        CoinText.text = ": " + goldController.GetGold();

        if (Input.GetButtonDown("SwapColor")){
            colorController.SwapColor();
            if (colorController.GetColor() == 1) {
                this.GetComponent<SpriteRenderer>().color = new Color(0.4f, 0.4f, 1f, 1f);
            }
            else {
                this.GetComponent<SpriteRenderer>().color = new Color(1f, 0.4f, 0.4f, 1f);
            }
        }


    }


    void OnTriggerEnter2D(Collider2D collider) {
        if (collider == null) { return; }
   
        //TODO : ajuster le tag après l'implémentation des projectiles)
        if (collider.CompareTag("ProjectileEnemy"))
        {
            lifeController.LoseLife(1);
            if (lifeController.GetLife() <= 0)
            {       
                Destroy(gameObject);
            }
        }
    }

}
