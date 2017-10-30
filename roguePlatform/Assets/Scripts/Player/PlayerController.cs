﻿using System.Collections;
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
    private GameManager gameManager;
    private GameObject objectManager;
    public GameObject healthBonus;
    ColorController colorController;



    [SerializeField]
    private int damageMelee = 1;
    [SerializeField]
    private int damageProjectile = 1;

    // All variables needed for SceneManager
    private bool endLevel;
    private bool shopDisabled;
    private bool isDead;
    // Use this for initialization
    void Start()
    {

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

        // Le player prend la couleur tweak dans unity
        colorController = gameObject.GetComponent<ColorController>();
        if (colorController.GetColor() == 1)
        {
            this.GetComponent<SpriteRenderer>().color = new Color(0.4f, 0.4f, 1f, 1f);
        }
        else
        {
            this.GetComponent<SpriteRenderer>().color = new Color(1f, 0.4f, 0.4f, 1f);
        }

    }

    // Update is called once per frame
    void Update()
    {
        Hud();
        SwapColor();
    }

    /// <summary>
    /// Affiche les informations hud
    /// </summary>
    private void Hud()
    {
        healthText.text = ": " + (lifeController.GetLife()) + " / " + lifeController.GetLifeMax();
        CoinText.text = ": " + goldController.GetEnergy() + " / " + goldController.GetEnergyMax();
    }

    /// <summary>
    /// Change le personnage de couleur (bleu ou rouge)
    /// Le mode bleu permet d'attaquer les ennemies bleu et inversement pour le mode rouge
    /// </summary>
    private void SwapColor()
    {

        if (Input.GetButtonDown("SwapColor"))
        {
            colorController.SwapColor();
            if (colorController.GetColor() == 1)
            {
                this.GetComponent<SpriteRenderer>().color = new Color(0.4f, 0.4f, 1f, 1f);
            }
            else
            {
                this.GetComponent<SpriteRenderer>().color = new Color(1f, 0.4f, 0.4f, 1f);
            }
        }
    }

    public int GetDamageMelee()
    {
        return damageMelee;
    }
    public int GetDamageProjectile()
    {
        return damageProjectile;
    }

    public void SetDamageMelee(int damage)
    {
        if (damage >= 0)
        {
            damageMelee = damage;
        }

    }

    public void SetDamageProjectile(int damage)
    {
        if (damage >= 0)
        {
            damageProjectile = damage;
        }

    }

    public bool GetIsDead()
    {
        return isDead;
    }

    public void SetIsDead(bool deadP)
    {
        isDead = deadP;
    }

    public void SetShopDisabled(bool shopDisabledP)
    {
        shopDisabled = shopDisabledP;
    }

    public bool GetShopDisabled()
    {
        return shopDisabled;
    }

    public bool GetEndLevel()
    {
        return endLevel;
    }

    public void SetEndLevel(bool endLevelP)
    {
        endLevel = endLevelP;
    }
}

