using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnPlayer : MonoBehaviour {

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!collider.CompareTag("Enemy") && !collider.CompareTag("GoldBonus") && !collider.CompareTag("HealthBonus") && !collider.CompareTag("Projectile"))
        {
            Destroy(gameObject);
        }
    }
}
