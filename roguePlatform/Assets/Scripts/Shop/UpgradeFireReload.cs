using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeFireReload : Upgrade{

    public GameObject player;
    private FireController fireController;
    public float upReload;
    public Button yourButton; 

    private void Start()
    {
        Button btn = yourButton.GetComponent<Button>(); ;
        btn.onClick.AddListener(effect); 
    }

    protected override void effect()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        fireController = player.GetComponent<FireController>();
        if (fireController.fireRate > 0.0f)
        {
            fireController.fireRate = fireController.fireRate - upReload;
        }
        else
            fireController.fireRate = 0; 
    }

	
}
