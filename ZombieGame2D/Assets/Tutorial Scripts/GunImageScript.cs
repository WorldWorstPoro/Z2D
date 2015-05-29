using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GunImageScript : MonoBehaviour {
	
	private int m_timer = 0;

	public Image m_gun_image;
	public Sprite m_pistol;
	public Sprite m_shotgun;
	public Sprite m_machineGun;

	public int first_transition;

	// Use this for initialization
	void Start () {
		m_gun_image = this.GetComponent<Image>();	
		m_gun_image.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
		m_timer++;
		
		if (m_timer == first_transition) {
			m_gun_image.enabled = true;
			m_gun_image.sprite = m_pistol;
		}
	}

	void FixedUpdate()
	{
	}
}
