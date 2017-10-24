using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeLife : Upgrade {

    private GameObject player;
    private LifeController lifeController;

    protected override void effect()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        lifeController = player.GetComponent<LifeController>();
        lifeController.SetLifeMax(lifeController.GetLifeMax() + 1); 
    }
}


