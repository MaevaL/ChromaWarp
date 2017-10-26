using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeFireDamage : Upgrade
{

    public Button yourButton;
    [SerializeField]
    private int fireDamage;
    private PlayerController playerController; 

    // Use this for initialization
    void Start()
    {
        Button btn = yourButton.GetComponent<Button>(); ;
        btn.onClick.AddListener(Temp);
    }

    protected override void Effect()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>(); 
        
        playerController.SetDamageProjectile(playerController.GetDamageProjectile() + fireDamage);
    }

}