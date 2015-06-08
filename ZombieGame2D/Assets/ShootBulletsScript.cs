using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ShootBulletsScript : MonoBehaviour {

	public Rigidbody2D m_bullet;

	public float max_speed;

	public float fireRate;
	public float reloadDelay;
	public int cur_gun;
	private float nextFire = 0.0F;

	public Sprite m_pistol;
	public Sprite m_shotgun;
	public Sprite m_machinegun;

	public int cur_bullets;
	public int max_bullets;

	private List<int> gunlist;

	private GameObject m_player;

	// Use this for initialization
	void Start (){
		gunlist = new List<int> ();
		cur_bullets = 0;
		gunlist.Add (cur_gun);

		m_player = GameObject.Find ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > nextFire && cur_bullets > 0) {
			if (Input.GetMouseButton (0))
			{
				nextFire = Time.time + fireRate;
				Vector3 mouse_pos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				Vector2 unit_direction = new Vector2 ((mouse_pos.x - m_player.transform.position.x), (mouse_pos.y - m_player.transform.position.y));
				unit_direction.Normalize ();
				//Determines angle
				float angle = Mathf.Atan2 (mouse_pos.y - m_player.transform.position.y, mouse_pos.x - m_player.transform.position.x) * Mathf.Rad2Deg;

				if (cur_gun != 2) {
					Rigidbody2D bullet_instance = Instantiate (m_bullet, transform.position, Quaternion.Euler (new Vector3 (0, 0, angle - 90))) as Rigidbody2D;
					bullet_instance.velocity = (unit_direction * max_speed);
					cur_bullets--;
				} else {
					Rigidbody2D bullet_instance1 = Instantiate (m_bullet, transform.position, Quaternion.Euler (new Vector3 (0, 0, angle - 90))) as Rigidbody2D;
					Rigidbody2D bullet_instance2 = Instantiate (m_bullet, transform.position, Quaternion.Euler (new Vector3 (0, 0, angle - 90))) as Rigidbody2D;
					Rigidbody2D bullet_instance3 = Instantiate (m_bullet, transform.position, Quaternion.Euler (new Vector3 (0, 0, angle - 90))) as Rigidbody2D;
					Rigidbody2D bullet_instance4 = Instantiate (m_bullet, transform.position, Quaternion.Euler (new Vector3 (0, 0, angle - 90))) as Rigidbody2D;
					Rigidbody2D bullet_instance5 = Instantiate (m_bullet, transform.position, Quaternion.Euler (new Vector3 (0, 0, angle - 90))) as Rigidbody2D;

					Vector2 unit_direction2 = unit_direction;
					Vector2 unit_direction3 = unit_direction;
					Vector2 unit_direction4 = unit_direction;
					Vector2 unit_direction5 = unit_direction;

					unit_direction2 = Quaternion.Euler (0, 0, 5) * unit_direction2;
					unit_direction3 = Quaternion.Euler (0, 0, -5) * unit_direction3;
					unit_direction4 = Quaternion.Euler (0, 0, 10) * unit_direction4;
					unit_direction5 = Quaternion.Euler (0, 0, -10) * unit_direction5;
	
					bullet_instance1.velocity = (unit_direction * max_speed);
					bullet_instance2.velocity = (unit_direction2 * max_speed);
					bullet_instance3.velocity = (unit_direction3 * max_speed);
					bullet_instance4.velocity = (unit_direction4 * max_speed);
					bullet_instance5.velocity = (unit_direction5 * max_speed);

					cur_bullets -= 5;
				}
			}

			ChangeReloadText(false);
		}


		if ( cur_bullets <= 0 ){
			nextFire = Time.time + reloadDelay;
			cur_bullets = max_bullets;
			ChangeReloadText(true);
		}

		if (Input.GetKeyDown (KeyCode.R)) {
			nextFire = Time.time + (reloadDelay * (1 - (cur_bullets * 1.0F / max_bullets * 1.0F)));
			cur_bullets = max_bullets;
			ChangeReloadText(true);
		}
		if (Input.GetKeyDown (KeyCode.Q)) {
			if(gunlist.Count > 1)
			{
				if ( cur_gun == gunlist[0] )
				{
					cur_gun = gunlist[gunlist.Count - 1];
				}
				else
				{
					cur_gun = gunlist[gunlist.IndexOf(cur_gun) - 1];
				}

				CurrentGunSelect();
			}
		}
		if (Input.GetKeyDown (KeyCode.E)) {
			if(gunlist.Count > 1)
			{
				if ( cur_gun == gunlist[gunlist.Count - 1] )
					cur_gun = gunlist[0]; 
				else
					cur_gun = gunlist[gunlist.IndexOf(cur_gun) + 1];     
			
				CurrentGunSelect();
			}
		}
	}
	public void AddGun (int i)
	{
		gunlist.Add (i);
	}

	public void CurrentGunSelect()
	{
		GameObject gun_img = GameObject.Find("GameGunImage");
		
		if ( cur_gun == 0 )
		{
			gun_img.GetComponent<Image>().sprite = m_pistol;
			fireRate = 0.4F;
			reloadDelay = 0.8F;
			max_bullets = 6;
			cur_bullets = 6;
			nextFire = Time.time + reloadDelay;
			ChangeReloadText(true);
		}
		else if ( cur_gun == 1)
		{
			gun_img.GetComponent<Image>().sprite = m_machinegun;
			fireRate = 0.1F;
			reloadDelay = 1.7F;
			max_bullets = 20;
			cur_bullets = 20;
			nextFire = Time.time + reloadDelay;
			ChangeReloadText(true);
		}
		else if ( cur_gun == 2)
		{
			gun_img.GetComponent<Image>().sprite = m_shotgun;
			fireRate = 0.9F;
			reloadDelay = 1.2F;
			max_bullets = 20;
			cur_bullets	= 20;
			nextFire = Time.time + reloadDelay;
			ChangeReloadText(true);
		}
	}

	public void ChangeReloadText(bool reloading)
	{
		GameObject reload_text = GameObject.Find ("AmmoText");

		if (!reloading)
			reload_text.GetComponent<Text> ().text = cur_bullets + "/" + max_bullets;
		else
			reload_text.GetComponent<Text> ().text = "Reloading...";
	}
}
