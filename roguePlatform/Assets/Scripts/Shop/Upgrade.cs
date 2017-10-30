using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Upgrade : MonoBehaviour {
    public GameObject player;
    public string title;
    public string description;
    public AudioClip soundClick;

    protected void Temp() {
        player = GameObject.FindGameObjectWithTag("Player");
        Buy(player.GetComponent<UpgradeController>().GetCost(), player.GetComponent<GoldController>().GetEnergy());
    }

    protected virtual void Buy(int coins, int playerCoins) {
        if (playerCoins >= coins) {
            playerCoins -= coins;
            player.GetComponent<GoldController>().SetEnergy(playerCoins);
            player.GetComponent<UpgradeController>().NewCost();
            SoundManager.instance.PlaySingle(soundClick);
            Effect();
        }
    }

    protected abstract void Effect();
}
