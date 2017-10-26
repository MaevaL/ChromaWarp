using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeFireDamage : Upgrade
{

    public Button yourButton;
    [SerializeField]
    private int fireDamage;
    private PlayerProjectile projectile; 

    // Use this for initialization
    void Start()
    {
        Button btn = yourButton.GetComponent<Button>(); ;
        btn.onClick.AddListener(Temp);
    }

    protected override void Effect()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
        //projectile = player.GetComponent<AttackTriggerController>();
        //fireDamage += fireDamage + projectile.GetDamageToEnemy();
        //projectile.SetDamageToEnemy(fireDamage);
    }

}