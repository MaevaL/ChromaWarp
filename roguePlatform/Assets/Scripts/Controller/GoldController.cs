using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  Gold controller
///  Manage gold point for Player
/// </summary>
public class GoldController : MonoBehaviour {

    [SerializeField]
    private int energy = 1;
    [SerializeField]
    private int energyMax = 1;

    public int GetEnergy() { return energy; }
    public int SetEnergy(int gold) { return energy = gold; }
    public int GetEnergyMax() { return energyMax; }
    public int SetGoldMax(int goldMax) { return energyMax = goldMax; }

    public void GainEnergy(int gain) {
        energy += gain;

        if (energy > energyMax) { energy = energyMax; }
    }

    public bool LoseEnergy(int loss) {
        energy -= loss;
        return (energy <= 0) ? false : true;
    }
}
