using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDieHandler : DieHandler {

    /// <summary>
    /// Call virtual method Die() from DieHandler class
    /// </summary>

    PlayerController playerController;
    private bool isDead; 
    

    internal override void Die() {
        playerController = gameObject.GetComponent<PlayerController>();
        playerController.SetIsDead(true);
        Debug.Log("Player is now Dead");  
    }

}
