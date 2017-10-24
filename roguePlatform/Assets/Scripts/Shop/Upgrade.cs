using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Upgrade : MonoBehaviour
{
    
    public int coins;
    public string title;
    public string description;

    

    public void buy (int coins, int playerCoins)
    {
        if (playerCoins >= coins)
        {
            playerCoins -= coins;
            effect(); 
        }
        //TODO Create else 
    }

    protected abstract void effect(); 

}
