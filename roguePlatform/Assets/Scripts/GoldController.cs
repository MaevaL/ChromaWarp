﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldController : MonoBehaviour {

    [SerializeField]
    private int _gold = 1;
    [SerializeField]
    private int _goldMax = 1;

    public int GetGold()
    {
        return _gold;
    }

    public int GetGoldMax()
    {
        return _goldMax;
    }

    public void GainGold(int gain)
    {
        _gold += gain;

        if (_gold > _goldMax)
        {
            _gold = _goldMax;
        }
    }

    public bool LoseGold(int loss)
    {
        _gold -= loss;

        if (_gold <= 0)
        {
            return false;
        }

        return true;
    }
}
