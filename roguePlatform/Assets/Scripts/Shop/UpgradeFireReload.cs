using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeFireReload : Upgrade{

   
    private FireController fireController;
    public float upReload;
    public Button yourButton;

    void Start()
    {
        Button btn = yourButton.GetComponent<Button>(); ;
        btn.onClick.AddListener(Temp);   
    }
   
    /// <summary>
    /// Substract The FireRate with a Parameter 
    /// </summary>
    protected override void Effect()
    {
        
        player = GameObject.FindGameObjectWithTag("Player");
        fireController = player.GetComponent<FireController>();
        if (fireController.GetFireRate() > 0.0f)
        {
            fireController.SetFireRate(fireController.GetFireRate() - upReload);
        }
        else
            fireController.SetFireRate(0);

      
    }

	
}
