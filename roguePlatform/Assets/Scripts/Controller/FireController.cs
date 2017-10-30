using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoguePlateformer;

/// <summary>
///  Fire controller
///  Manage the distant attack of the Player
/// </summary>
public class FireController : MonoBehaviour {
    [SerializeField]
    private Transform _bulletSpawner = null;
    [SerializeField]
    private float fireRate = 1f;
    [SerializeField]
    private float _animShootDuration;

    private Animator anim;
    private bool canShoot = true;
    private PlayerMove _move;

    public AudioClip fireAudio;
    public GameObject blueProjectile;
    public GameObject redProjectile;
    public Vector2 velocity;

    void Start() {
        _move = GetComponent<PlayerMove>();
        anim = GetComponent<Animator>();
    }

    void Update() {
        Fire();
    }

    private void Fire() {
        //Player can only do a distant attack when his color is blue(1)
        if (Input.GetButtonDown("Fire") && canShoot && GetComponent<ColorController>().GetColor() == 1) {
            Vector2 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 diff = (worldPos - (Vector2)_bulletSpawner.position).normalized;

            if (MouseFrontOfPlayer(worldPos)) {
                StartCoroutine(RecoveryShot(diff));
                canShoot = false;
            }
        }
    }

    /// <summary>
    /// Delay between 2 shoot
    /// </summary>
    IEnumerator RecoveryShot(Vector3 hit) {
        anim.SetTrigger("ShootT");
        yield return new WaitForSeconds(fireRate / 2);
        GameObject go;
        //Generate a blue projectile if the player is blue
        if (GetComponent<ColorController>().GetColor() == 1) {
            go = Instantiate(blueProjectile, _bulletSpawner.position, Quaternion.identity) as GameObject;
            SoundManager.instance.PlaySingle(fireAudio);
        }
        else {
        //Generate a red projectile if the player is red
            go = Instantiate(redProjectile, _bulletSpawner.position, Quaternion.identity) as GameObject;
            SoundManager.instance.PlaySingle(fireAudio);
        }
        //Set the speed and the amount of damage to the projectile
        go.GetComponent<Rigidbody2D>().velocity = new Vector2(velocity.x * (hit.x), (hit.y) * velocity.y);
        go.GetComponent<PlayerProjectile>().Damages = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().GetDamageProjectile();
        yield return new WaitForSeconds(fireRate / 2);
        canShoot = true;
    }

    /// <summary>
    /// Check if the mouse is on the same side that player view
    /// Return true if they are in the same side
    /// </summary>
    private bool MouseFrontOfPlayer(Vector3 hit) {
        return ((hit.x > transform.position.x && _move.facingRight)
            || (hit.x < transform.position.x && !_move.facingRight));
    }

    public float GetFireRate() { return fireRate; }
    public void SetFireRate(float fireRateP) { fireRate = fireRateP; }
}
