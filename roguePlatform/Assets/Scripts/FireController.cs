using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoguePlateformer;

public class FireController : MonoBehaviour {
    
    public GameObject projectile;
    public Vector2 velocity;
    bool canShoot = true;
    public Vector2 offset = new Vector2(0.4f, 0.1f);
    public float fireRate = 1f;
    private PlayerMove _move;
   

    // Use this for initialization
    void Start()
    {
        _move = GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Fire") && canShoot) {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && MouseFrontOfPlayer(hit)) {
                //Mettre un as Gameobject permet a unity de créer directement un GameObject.
                //Et pas de créer un Object pour ensuite le cast en GameObject.
                GameObject go = Instantiate(projectile, (Vector2)transform.position + offset * transform.localScale.x, Quaternion.identity) as GameObject;

                go.GetComponent<Rigidbody2D>().velocity = new Vector2(velocity.x * (hit.point.x - transform.position.x), (hit.point.y - transform.position.y) * velocity.y);
            }


            canShoot = false;
            StartCoroutine(RecoveryShot());
        }
    }

    IEnumerator RecoveryShot()
    {
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }
    
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
