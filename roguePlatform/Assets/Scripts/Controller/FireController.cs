using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoguePlateformer;

public class FireController : MonoBehaviour {

    public GameObject blueProjectile;
    public GameObject redProjectile;
    public Vector2 velocity;
    bool canShoot = true;
    [SerializeField]
    private Transform _bulletSpawner = null;
    [SerializeField]
    private float fireRate = 1f;
    private PlayerMove _move;
    [SerializeField]
    private float _animShootDuration;
    private Animator anim;
    public AudioClip fireAudio;


    // Use this for initialization
    void Start() {
        _move = GetComponent<PlayerMove>();
        anim = GetComponent<Animator>(); 
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

            Vector2 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 diff = (worldPos - (Vector2) _bulletSpawner.position).normalized;
            

            if (MouseFrontOfPlayer(worldPos)) {
                //Mettre un as Gameobject permet a unity de créer directement un GameObject.
                //Et pas de créer un Object pour ensuite le cast en GameObject.
                StartCoroutine(RecoveryShot(diff));
                canShoot = false; 
                 
            }
        }
    }

    /// <summary>
    /// Délai entre deux tirs
    /// </summary>
    /// <returns></returns>
    IEnumerator RecoveryShot(Vector3 hit) {
 
        anim.SetTrigger("ShootT");
        yield return new WaitForSeconds(fireRate / 2);
        GameObject go;
        if (GetComponent<ColorController>().GetColor() == 1) {
             go = Instantiate(blueProjectile , _bulletSpawner.position , Quaternion.identity) as GameObject;
            SoundManager.instance.PlaySingle(fireAudio);
        } else {
            go = Instantiate(redProjectile , _bulletSpawner.position , Quaternion.identity) as GameObject;
            SoundManager.instance.PlaySingle(fireAudio);
        }
        
        go.GetComponent<Rigidbody2D>().velocity = new Vector2(velocity.x * (hit.x), (hit.y) * velocity.y);
        go.GetComponent<PlayerProjectile>().Damages = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().GetDamageProjectile();
        yield return new WaitForSeconds(fireRate / 2 );
     
        canShoot = true;
    }

    /// <summary>
    /// Retourne vrai si le joueur va a gauche et est tourné du côté gauche 
    /// et inversement avec le côté droit 
    /// sinon renvoi faux
    /// </summary>
    /// <param name="hit"></param>
    /// <returns></returns>
    private bool MouseFrontOfPlayer(Vector3 hit) {
        /*
        if(hit.point.x > transform.position.x && _move.facingRight) {
            return true;
        }

        if (hit.point.x < transform.position.x && !_move.facingRight) {
            return true;
        }

        return false;
        */

        return ((hit.x > transform.position.x && _move.facingRight)
            || (hit.x < transform.position.x && !_move.facingRight));
    }

    public float GetFireRate()
    {
        return fireRate; 
    }

    public void SetFireRate(float fireRateP)
    {
        fireRate = fireRateP; 
    }
}
