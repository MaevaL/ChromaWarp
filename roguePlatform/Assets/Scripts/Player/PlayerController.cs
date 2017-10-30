using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class PlayerController : MonoBehaviour {

    private LifeController lifeController;
    private GameManager gameManager;
    private GameObject objectManager;
    private GoldController goldController;
    private ColorController colorController;

    [SerializeField]
    private int damageMelee = 1;
    [SerializeField]
    private int damageProjectile = 1;

    public Text healthText;
    public Text CoinText;
    public GameObject healthBonus;

    // All variables needed for SceneManager
    private bool endLevel;
    private bool shopDisabled;
    private bool isDead;

    void Start() {
        objectManager = GameObject.FindGameObjectWithTag("GameManager");
        gameManager = objectManager.GetComponent<GameManager>();
        gameManager.InitialisationPlayer();
        endLevel = false;
        isDead = false;
        shopDisabled = false;
        lifeController = gameObject.GetComponent<LifeController>();
        healthText = GameObject.Find("HealthText").GetComponent<Text>();
        healthText.text = ": " + lifeController.GetLife() + " / " + lifeController.GetLifeMax();

        goldController = gameObject.GetComponent<GoldController>();
        CoinText = GameObject.Find("CoinText").GetComponent<Text>();
        CoinText.text = ": " + goldController.GetEnergy() + " / " + goldController.GetEnergyMax();

        // player's color is define in the inspector 
        colorController = gameObject.GetComponent<ColorController>();
        if (colorController.GetColor() == 1) {
            this.GetComponent<SpriteRenderer>().color = new Color(0.4f, 0.4f, 1f, 1f);
        } else {
            this.GetComponent<SpriteRenderer>().color = new Color(1f, 0.4f, 0.4f, 1f);
        }
    }

    void Update() {
        Hud();
        SwapColor();
    }

    private void Hud() {
        healthText.text = ": " + (lifeController.GetLife()) + " / " + lifeController.GetLifeMax();
        CoinText.text = ": " + goldController.GetEnergy() + " / " + goldController.GetEnergyMax();
    }

    private void SwapColor() {
        if (Input.GetButtonDown("SwapColor")) {
            colorController.SwapColor();
            if (colorController.GetColor() == 1) {
                this.GetComponent<SpriteRenderer>().color = new Color(0.4f, 0.4f, 1f, 1f);
            } else {
                this.GetComponent<SpriteRenderer>().color = new Color(1f, 0.4f, 0.4f, 1f);
            }
        }
    }

    public int GetDamageMelee() { return damageMelee; }
    public int GetDamageProjectile() { return damageProjectile; }

    public void SetDamageMelee(int damage) {
        if (damage >= 0) { damageMelee = damage; }
    }

    public void SetDamageProjectile(int damage) {
        if (damage >= 0) { damageProjectile = damage; }
    }

    public bool GetIsDead() { return isDead; }

    public void SetIsDead(bool deadP) { isDead = deadP; }

    public void SetShopDisabled(bool shopDisabledP) { shopDisabled = shopDisabledP; }

    public bool GetShopDisabled() { return shopDisabled; }

    public bool GetEndLevel() { return endLevel; }

    public void SetEndLevel(bool endLevelP) { endLevel = endLevelP; }
}

