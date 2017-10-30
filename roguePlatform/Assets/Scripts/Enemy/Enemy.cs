using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  Enemy controller
///  Initialisation of Enemy
/// </summary>
public class Enemy : MonoBehaviour {

    private LifeController lifeController;
    private ColorController colorController;
    private MoveController moveController;
    public int damageMelee = 1;
    public int damageProjectile = 1;

    void Start() {
        colorController = gameObject.GetComponent<ColorController>();
        if (colorController.GetColor() == 1) {
            this.GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 1f, 1f);
        } else {
            this.GetComponent<SpriteRenderer>().color = new Color(1f, 0.4f, 0.4f, 1f);
        }
        lifeController = gameObject.GetComponent<LifeController>();
    }
}