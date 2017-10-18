using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bonus : MonoBehaviour {
    protected GameObject player;
    public abstract void SetBonus(int arg);    
}
