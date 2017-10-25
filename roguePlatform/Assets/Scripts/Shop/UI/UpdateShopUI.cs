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
    public GameObject player;
    //Controller 
    private UpgradeController upgradeController;
    private GoldController goldController;
    private LifeController lifeController;
    private FireController fireController; 
    //Graphics
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
        //Initialize all text fields
        neededEnergy.text = "Energy needed for next upgrade : " + upgradeController.GetCost();
        currentEnergy.text = "Current Energy : " + goldController.GetGold() + "/" + goldController.GetGoldMax();
        maxLife.text = "" + lifeController.GetLifeMax();
        fireRate.text = fireController.fireRate + "/sec";
        maxEnergy.text = "" + goldController.GetGoldMax(); 
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
        currentEnergy.text = "Current Energy : " + goldController.GetGold() + "/" + goldController.GetGoldMax();
        maxLife.text = "" + lifeController.GetLifeMax();
        fireRate.text = fireController.fireRate + "/sec";
        maxEnergy.text = "" + goldController.GetGoldMax();
    }

    private void Exit()
    {
        //Disable the HUD UPGRADE
        hudUpgrade.gameObject.SetActive(false); 
    }

        

}
