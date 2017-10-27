using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEnemy : MonoBehaviour {

    public GameObject blueProjectile;
    public GameObject redProjectile;
    public Vector2 velocity;
    bool canShoot = true;
    public Vector2 offset = new Vector2(0.4f, 0.1f);
    public float fireRate = 1f;
    public float delay = 1f;
    private Animator anim;

    //Turret
    private GameObject target;
    public float distancePlayer;
    public Transform shootPointLeft;
    public Transform shootPointRight;


    // Use this for initialization
    void Start() {
        anim = GetComponent<Animator>();

        if (gameObject.GetComponent<MoveController>().type == 4) {
            target = GameObject.FindGameObjectWithTag("Player");
            //InvokeRepeating("TurretFire", delay, fireRate);
        }
        else {
            InvokeRepeating("Fire", delay, fireRate);
        }
    }

    /// <summary>
    /// Attaque à distance de l'ennemi qui lance des projectiles
    /// </summary>
    private void Fire() {
        anim.SetTrigger("ShootEnemyT");

        GameObject go;
        if (GetComponent<ColorController>().GetColor() == 1) {
            go = Instantiate(blueProjectile, (Vector2)transform.position + offset * transform.localScale.x, Quaternion.identity) as GameObject;
        }
        else {
            go = Instantiate(redProjectile, (Vector2)transform.position + offset * transform.localScale.x, Quaternion.identity) as GameObject;
        }
        go.GetComponent<EnemyProjectile>().Damages = gameObject.GetComponent<Enemy>().damageProjectile;
        go.GetComponent<Rigidbody2D>().velocity = new Vector2(velocity.x * transform.localScale.x, velocity.y);
    }

    public void TurretFire(bool attackingRight) {
        anim.SetTrigger("ShootEnemyT");
        Vector3 positionTargetCompense = new Vector3(target.transform.position.x, target.transform.position.y * target.GetComponent<BoxCollider2D>().offset.y, 0);
        Vector2 direction = positionTargetCompense - transform.position;
        direction.Normalize();
        
        GameObject go;

        if (!attackingRight) {

            if (GetComponent<ColorController>().GetColor() == 1) {
                go = Instantiate(blueProjectile, shootPointRight.transform.position, Quaternion.identity) as GameObject;
            }
            else {
                go = Instantiate(redProjectile, shootPointRight.transform.position, Quaternion.identity) as GameObject;
            }


        }
        else {

            if (GetComponent<ColorController>().GetColor() == 1) {
                go = Instantiate(blueProjectile, shootPointLeft.transform.position, Quaternion.identity) as GameObject;
            }
            else {
                go = Instantiate(redProjectile, shootPointLeft.transform.position, Quaternion.identity) as GameObject;
            }

        }
        go.GetComponent<EnemyProjectile>().Damages = gameObject.GetComponent<Enemy>().damageProjectile;
        go.GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x*velocity.x, direction.y * velocity.y);
    }
}
