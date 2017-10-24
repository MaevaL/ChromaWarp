using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAmelioration : Amelioration {
    private void Start() {
        setAmelioration("0.001");
    }
    public override void setAmelioration(string param) {
        FireController fireController = GameObject.FindWithTag("Player").GetComponent<FireController>();
        fireController.fireRate = (float)System.Convert.ToDouble((param));
    }
}
