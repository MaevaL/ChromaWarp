using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePunchDamage : Upgrade {

    private PlayerController playerController;

    [SerializeField]
    private int punchDamage;

    public Button yourButton;

    protected override void Effect() {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();

        playerController.SetDamageMelee(playerController.GetDamageMelee() + punchDamage);
    }

    void Start() {
        Button btn = yourButton.GetComponent<Button>(); ;
        btn.onClick.AddListener(Temp);
    }
}
