using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDieHandler : DieHandler {

    /// <summary>
    /// Appelle le comportement de la méthode virtuelle Die() de la classe DieHandler 
    /// </summary>

    PlayerController playerController;
    private bool isDead; 
    

    internal override void Die() {
        playerController = gameObject.GetComponent<PlayerController>();
        playerController.SetIsDead(true);
        Debug.Log("Player is now Dead"); 
  //    base.Die(); 
    }

}
