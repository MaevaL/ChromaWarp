using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Initialize a Cost and Facteur to increase 
/// the global cost of all the upgrade
/// </summary>
public class UpgradeController : MonoBehaviour
{

    [SerializeField]
    private int cost;
    [SerializeField]
    private float facteur; 

    public int GetCost()
    {
        return cost;
    }

    public void NewCost()
    {
        cost = (int) (cost * facteur);
    }
}


