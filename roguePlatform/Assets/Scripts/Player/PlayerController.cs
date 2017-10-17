using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class PlayerController : MonoBehaviour
{

    LifeController lifeController;

    // Use this for initialization
    void Start()
    {

        lifeController = gameObject.GetComponent<LifeController>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }
}
