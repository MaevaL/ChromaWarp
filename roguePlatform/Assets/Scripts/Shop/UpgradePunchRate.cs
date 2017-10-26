using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePunchRate : Upgrade {

    // Use this for initialization
    private MeleeController meleeController;
    public float melee;
    public Button yourButton; 

	void Start () {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(Temp); 
	}

    protected override void Effect()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        meleeController = player.GetComponent<MeleeController>();
        meleeController.SetAttackCoolDown(meleeController.GetAttackCooldown() - melee); 
    }
}
