using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBonus : Bonus {

    private void Awake() {
        player = FindObjectOfType<PlayerController>().gameObject;
        Debug.Log(player.name);
        
    }
    public override void SetBonus(int arg) {
        player.GetComponent<LifeController>().GainLife(arg);
    }
}
