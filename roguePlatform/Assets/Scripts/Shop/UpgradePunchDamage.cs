using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePunchDamage : Upgrade {

    public Button yourButton;
    private float punchDamage;
    private MeleeController meleeController; 

    protected override void Effect()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        meleeController = player.GetComponent<MeleeController>(); 
        
    }

    // Use this for initialization
    void Start () {
        Button btn = yourButton.GetComponent<Button>(); ;
        btn.onClick.AddListener(Temp);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
