using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  Color controller
///  Manage the color of the Player & Enemy
/// </summary>
public class ColorController : MonoBehaviour {

    [SerializeField]
    private int _color;

    public int GetColor() { return _color; }

    //Change the color between blue(1) and red(2) 
    public void SwapColor() {
        if (_color == 1) {
            _color = 2;
        }
        else if (_color == 2) {
            _color = 1;
        }
    }

    //Check if the color is the same between 2 objects
    //white(0) is the same as blue and red
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
