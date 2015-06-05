using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BulletBasicBehavior : MonoBehaviour {

	int score = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//Destroys the bullet on collision with terrain or with a zombie
	void OnTriggerEnter2D (Collider2D col) 
	{
		if (col.tag == "Terrain" || col.tag == "Walls") {

			//Destroy the bullet
			Destroy(gameObject);
		}

		if (col.tag == "Zombie") {
			Destroy(col.gameObject);
			Destroy(gameObject);
			
			GameObject gis = GameObject.Find("Canvas");

			score = int.Parse(gis.GetComponent<HUDScript>().MoneyText.GetComponent<Text>().text);

			score += 100;

			gis.GetComponent<HUDScript>().MoneyText.GetComponent<Text>().text = score.ToString();
		}
	}
}
