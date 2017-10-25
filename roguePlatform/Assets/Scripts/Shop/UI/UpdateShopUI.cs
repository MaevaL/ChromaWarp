using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateShopUI : MonoBehaviour {

    public Text neededEnergy;
    public Text currentEnergy; 
    public GameObject player;
    private UpgradeController upgradeController;
    private GoldController goldController;
    [SerializeField]
    private Button yourButton;
    [SerializeField]
    private Canvas hudUpgrade; 
    // Use this for initialization
    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player");
        upgradeController = player.GetComponent<UpgradeController>();
        goldController = player.GetComponent<GoldController>();
        neededEnergy.text = "Energy needed for next upgrade : " + upgradeController.GetCost();
        currentEnergy.text = "Current Energy : " + goldController.GetGold() + "/" + goldController.GetGoldMax();
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(Exit); 

    }
	// Update is called once per frame
	void Update () {
        Hud(); 
	}
    
    private void Hud()
    {
        neededEnergy.text = "Energy needed for next upgrade : " + upgradeController.GetCost();
        currentEnergy.text = "Current Energy : " + goldController.GetGold() + "/" + goldController.GetGoldMax();
    }

    private void Exit()
    {
        hudUpgrade.gameObject.SetActive(false); 
    }

        

}
