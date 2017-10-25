using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Upgrade : MonoBehaviour
{
    
    private float facteur = 2f;
    public GameObject player; 
    public string title;
    public string description;
   
    
    //public Button yourButton; 

    void Start()
    {
        // For children 
        //Button btn = yourButton.GetComponent<Button>(); ;
        //btn.onClick.AddListener(Temp);

    }

    protected void Temp()
    {     
        player = GameObject.FindGameObjectWithTag("Player");
        Buy(player.GetComponent<UpgradeController>().GetCost(), player.GetComponent<GoldController>().GetGold()); 
    }

    protected virtual void Buy(int coins, int playerCoins)
    {
 
        if (playerCoins >= coins)
        {
            playerCoins -= coins;
            player.GetComponent<GoldController>().SetGold(playerCoins);
            player.GetComponent<UpgradeController>().NewCost(); 
            Effect();
        }
        //TODO Create else 
    }

    protected abstract void Effect(); 

  
}
