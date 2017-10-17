using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeController : MonoBehaviour
{

    [SerializeField]
    private int _life = 1;

    public int GetLife()
    {
        return _life;
    }

    public void GainLife(int gain)
    {
        _life += gain;
    }

    public bool LoseLife(int loss)
    {
        _life -= loss;

        if (_life <= 0)
        {
            return false;
        }

        return true;
    }
}
