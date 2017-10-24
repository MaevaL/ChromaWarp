using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {


    LifeController lifeController;
    ColorController colorController;
    MoveController moveController;

    void Start() {
        colorController = gameObject.GetComponent<ColorController>();
        if (colorController.GetColor() == 1) {
            this.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 1f, 1f);
        } else {
            this.GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f, 1f);
        }
        lifeController = gameObject.GetComponent<LifeController>();
    }
}