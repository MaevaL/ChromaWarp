using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEnemy : MonoBehaviour {


    public GameObject projectile;
    public Vector2 velocity;
    bool canShoot = true;
    public Vector2 offset = new Vector2(0.4f, 0.1f);
    public float fireRate = 1f;
    public float delay = 1f;
    private Animator anim;



    // Use this for initialization
    void Start() {
        anim = GetComponent<Animator>();
        InvokeRepeating("Fire", delay, fireRate);
    }

    /// <summary>
    /// Attaque à distance de l'ennemi qui lance des projectiles
    /// </summary>
    private void Fire() {
        anim.SetTrigger("ShootEnemyT");
        GameObject go = (GameObject)Instantiate(projectile, (Vector2)transform.position + offset * transform.localScale.x, Quaternion.identity);
        go.GetComponent<Rigidbody2D>().velocity = new Vector2(velocity.x * transform.localScale.x, velocity.y);
    }
}
