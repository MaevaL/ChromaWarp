using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeAmelioration : Amelioration {

    public Button yourButton;

    private void Start()
    {
  //      Button btn = yourButton.GetComponent<Button>(); ;
    //    btn.onClick.AddListener(setAmelioration);
        
    }


    public override void setAmelioration() {
        Debug.Log("life"); 
        LifeController life = GameObject.FindWithTag("Player").GetComponent<LifeController>();
        life.SetLifeMax(System.Convert.ToInt32(param));
        life.SetLife(System.Convert.ToInt32(param));
        //Debug.Log(life.GetLifeMax());
        //Debug.Log(life.GetLife());
    }
}
