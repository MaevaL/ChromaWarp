using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMaxCoins : Upgrade {

    private GoldController goldController;

    [SerializeField]
    private int goldMax;

    public Button yourButton;
    
    void Start() {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(Temp);
    }

    protected override void Effect() {
        int currentGoldMax;

        player = GameObject.FindGameObjectWithTag("Player");
        goldController = player.GetComponent<GoldController>();
        currentGoldMax = goldController.GetEnergyMax();
        goldController.SetGoldMax(currentGoldMax + goldMax);
    }
}
