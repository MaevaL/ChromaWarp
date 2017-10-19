using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour {
    [SerializeField]
    private float projectileSpeed = 5f;
    public GameObject projectile;
    public GameObject Emitter; 
    private Vector2 position; 
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Jump"))
        {
            position = new Vector2(Emitter.transform.position.x +0.1f, Emitter.transform.position.y); 
            GameObject bullet = GameObject.Instantiate(projectile, position , Quaternion.identity);
            bullet.transform.Translate(new Vector3(0, 1) * Time.deltaTime * projectileSpeed);
          
        }
	}
}
