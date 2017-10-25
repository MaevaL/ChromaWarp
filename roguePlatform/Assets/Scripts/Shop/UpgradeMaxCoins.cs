using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class UpgradeMaxCoins : Upgrade
{

    private GoldController goldController;
    public Button yourButton;
    [SerializeField]
    private int goldMax;

    // Use this for initialization
    void Start()
    {
        Button btn = yourButton.GetComponent<Button>(); 
        btn.onClick.AddListener(Temp);
    }

    protected override void Effect()
    {
        int currentGoldMax;

        player = GameObject.FindGameObjectWithTag("Player");
        goldController = player.GetComponent<GoldController>();
        currentGoldMax = goldController.GetGoldMax();
        goldController.SetGoldMax(currentGoldMax + goldMax);
        Debug.Log(goldController.GetGoldMax()); 
    }
}
