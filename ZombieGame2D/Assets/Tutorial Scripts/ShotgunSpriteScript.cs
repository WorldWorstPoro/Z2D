using UnityEngine;
using System.Collections;

public class ShotgunSpriteScript : MonoBehaviour {

	private int m_timer = 0;
	private Collider2D m_col;

	public SpriteRenderer m_image;

	// Use this for initialization
	void Start () {
		m_image = this.GetComponent<SpriteRenderer> ();
		m_col = this.GetComponent<Collider2D> ();

		m_col.enabled = false;
		m_image.enabled = false;

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		m_timer++;

		if (m_timer == 2600) {
			m_col.enabled = true;
			m_image.enabled = true;
		}
	}

	void OnTriggerEnter2D (Collider2D col) 
	{
		if (col.tag == "Player") {

			GameObject gis = GameObject.Find("GunImage");
			gis.GetComponent<GunImageScript>().m_gun_image.sprite = m_image.sprite;

			//Destroy the gun
			Destroy(gameObject);
		}
	}
}
