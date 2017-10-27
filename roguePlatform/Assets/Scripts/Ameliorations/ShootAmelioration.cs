using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAmelioration : Amelioration {
    private void Start() {
        setAmelioration();
    }
    public override void setAmelioration() {
        FireController fireController = GameObject.FindWithTag("Player").GetComponent<FireController>();
        fireController.SetFireRate((float)System.Convert.ToDouble((0.0001)));
    }
}
