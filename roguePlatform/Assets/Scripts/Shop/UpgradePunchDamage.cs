using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePunchDamage : Upgrade {


    public Button yourButton;
    [SerializeField]
    private float punchDamage;
    private PlayerController playerController; 

    protected override void Effect()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>(); 
        //TODO Implement mechanism to upgrade the punchDamage
    }

    // Use this for initialization
    void Start () {
        Button btn = yourButton.GetComponent<Button>(); ;
        btn.onClick.AddListener(Temp);
    }

  
}
