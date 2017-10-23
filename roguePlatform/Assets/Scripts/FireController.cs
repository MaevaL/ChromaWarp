using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoguePlateformer;

public class FireController : MonoBehaviour {

    public GameObject projectile;
    public Vector2 velocity;
    bool canShoot = true;
    [SerializeField]
    private Transform _bulletSpawner = null;
    public float fireRate = 1f;
    private PlayerMove _move;
    

    // Use this for initialization
    void Start() {
        _move = GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    void Update() {
        Fire();
    }

    /// <summary>
    /// Réalise l'action de tiré en fontion d'un input clavier et d'une direction de la souris
    /// </summary>
    private void Fire() {
        if (Input.GetButtonDown("Fire") && canShoot) {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && MouseFrontOfPlayer(hit)) {
                //Mettre un as Gameobject permet a unity de créer directement un GameObject.
                //Et pas de créer un Object pour ensuite le cast en GameObject.
                GameObject go = Instantiate(projectile, _bulletSpawner.position, Quaternion.identity) as GameObject;

                go.GetComponent<Rigidbody2D>().velocity = new Vector2(velocity.x * (hit.point.x - transform.position.x), (hit.point.y - transform.position.y) * velocity.y);
            }

            canShoot = false;
            StartCoroutine(RecoveryShot());
        }
    }

    /// <summary>
    /// Délai entre deux tirs
    /// </summary>
    /// <returns></returns>
    IEnumerator RecoveryShot() {
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }

    /// <summary>
    /// Retourne vrai si le joueur va a gauche et est tourné du côté gauche 
    /// et inversement avec le côté droit 
    /// sinon renvoi faux
    /// </summary>
    /// <param name="hit"></param>
    /// <returns></returns>
    private bool MouseFrontOfPlayer(RaycastHit hit) {
        /*
        if(hit.point.x > transform.position.x && _move.facingRight) {
            return true;
        }

        if (hit.point.x < transform.position.x && !_move.facingRight) {
            return true;
        }

        return false;
        */

        return ((hit.point.x > transform.position.x && _move.facingRight)
            || (hit.point.x < transform.position.x && !_move.facingRight));
    }



}
