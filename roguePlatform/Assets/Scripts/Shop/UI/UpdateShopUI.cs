using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateShopUI : MonoBehaviour {

    [SerializeField]
    private Text fireRate;
    [SerializeField]
    private Text maxEnergy;  
    [SerializeField]
    private Text neededEnergy;
    [SerializeField]
    private Text currentEnergy;
    [SerializeField]
    private Text maxLife;
    [SerializeField]
    private Text fireDamage;
    [SerializeField]
    private Text punchDamage;
    [SerializeField]
    private Text punchRate; 

    public GameObject player;
    //Controller 
    private UpgradeController upgradeController;
    private GoldController goldController;
    private LifeController lifeController;
    private FireController fireController;
    private AttackTriggerController attackTriggerController;
    private PlayerController playerController;
    private MeleeController meleeController;

  //Other Graphics
    [SerializeField]
    private Button yourButton;
    [SerializeField]
    private Canvas hudUpgrade; 
    // Use this for initialization
    void Start()
    {
        //Get All Controller 
        player = GameObject.FindGameObjectWithTag("Player");
        upgradeController = player.GetComponent<UpgradeController>();
        goldController = player.GetComponent<GoldController>();
        lifeController = player.GetComponent<LifeController>();
        fireController = player.GetComponent<FireController>();
        playerController = player.GetComponent<PlayerController>();
        meleeController = player.GetComponent<MeleeController>();
        //Initialize all text fields
        Hud();
        //neededEnergy.text = "Energy needed for next upgrade : " + upgradeController.GetCost();
        //currentEnergy.text = "Current Energy : " + goldController.GetGold() + "/" + goldController.GetGoldMax();
        //maxLife.text = "" + lifeController.GetLifeMax();
        //fireRate.text = "each " + fireController.fireRate + " sec";
        //maxEnergy.text = "" + goldController.GetGoldMax();
        //fireDamage.text = "Fire Damage" + attackTriggerController.GetDamageToEnemy();  
    //Create Listener EXIT 
    Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(Exit); 

    }
	// Update is called once per frame
	void Update () {
        Hud(); 
	}
    
    private void Hud()
    {
        //Update all text fields 
        neededEnergy.text = "Energy needed for next upgrade : " + upgradeController.GetCost();
        currentEnergy.text = "Current Energy : " + goldController.GetEnergy() + "/" + goldController.GetEnergyMax();
        maxLife.text = "" + lifeController.GetLifeMax();
        fireRate.text = "each " + fireController.GetFireRate() + " sec";
        maxEnergy.text = "" + goldController.GetEnergyMax();
        fireDamage.text = "Fire Damage " + playerController.GetDamageProjectile();
        punchDamage.text = "Punch Damage " + playerController.GetDamageMelee();
        punchRate.text = "each" + meleeController.GetAttackCooldown() + " sec"; 
    }

    private void Exit()
    {
        //Disable the HUD UPGRADE
        hudUpgrade.gameObject.SetActive(false); 
    }

        

}
