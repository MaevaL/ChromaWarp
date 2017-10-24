using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorController : MonoBehaviour {

    [SerializeField]
    private int _color;

    public int GetColor() {
        return _color;
    }

    public void SwapColor() {
        if (_color == 1) {
            _color = 2;
            Debug.Log("blue to red" + _color);
        }
        else if (_color == 2) {
            _color = 1;
            Debug.Log("red to blue" + _color);
        }

    }

    public bool SameColor(int color) {

        if (_color == color) {
            return true;
        }
        else if (_color == 0 || color == 0) {
            return true;
        }

        return false;
    }

}
