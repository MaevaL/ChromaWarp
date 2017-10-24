using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeAmelioration : Amelioration {
    private void Start() {
        setAmelioration("1");
    }
    public override void setAmelioration(string param) {
        LifeController life = GameObject.FindWithTag("Player").GetComponent<LifeController>();
        life.SetLifeMax(System.Convert.ToInt32(param));
        life.SetLife(System.Convert.ToInt32(param));
        Debug.Log(life.GetLifeMax());
        Debug.Log(life.GetLife());
    }
}
