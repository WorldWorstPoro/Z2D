using UnityEngine;
using System.Collections;

public class BulletBasicBehavior : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//Destroys the bullet on collision with terrain or with a zombie
	void OnTriggerEnter2D (Collider2D col) 
	{
		if (col.tag == "Zombie" || col.tag == "Terrain") {

			//Destroy the bullet
			Destroy(gameObject);
		}
	}
}
