using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeFireDamage : Upgrade {

    private PlayerController playerController;

    [SerializeField]
    private int fireDamage;

    public Button yourButton;
    
    void Start() {
        Button btn = yourButton.GetComponent<Button>(); ;
        btn.onClick.AddListener(Temp);
    }

    protected override void Effect() {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        playerController.SetDamageProjectile(playerController.GetDamageProjectile() + fireDamage);
    }

}