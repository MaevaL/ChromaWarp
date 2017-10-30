using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeLife : Upgrade {


    private LifeController lifeController;

    [SerializeField]
    private int life;

    public Button yourButton;

    private void Start() {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(Temp);
    }

    /// <summary>
    /// Add life corresponding to parameters
    /// </summary>
    protected override void Effect() {
        player = GameObject.FindGameObjectWithTag("Player");
        lifeController = player.GetComponent<LifeController>();
        lifeController.SetLifeMax(lifeController.GetLifeMax() + life);

        lifeController.SetLife(lifeController.GetLifeMax());
    }
}


