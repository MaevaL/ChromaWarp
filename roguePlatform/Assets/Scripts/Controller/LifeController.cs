using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  Life controller
///  Manage life point for Player and Enemy
/// </summary>
public class LifeController : MonoBehaviour {

    [SerializeField]
    private int _life = 1;
    [SerializeField]
    private int _lifeMax = 1;

    public int GetLife() { return _life; }
    public int GetLifeMax() { return _lifeMax; }
    public void SetLife(int life) { _life = life; }
    public void SetLifeMax(int gain) { _lifeMax = gain; }

    public void GainLife(int gain) {
        _life += gain;

        if (_life > _lifeMax) {
            _life = _lifeMax;
        }
    }

    public bool LoseLife(int loss) {
        _life -= loss;

        if (_life <= 0) {
            GetComponent<DieHandler>().Die();
            return false;
        }
        return true;
    }
}