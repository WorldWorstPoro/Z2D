using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShootBulletsScript : MonoBehaviour {

	public Rigidbody2D m_bullet;

	public float max_speed;

	public float fireRate;
	public float reloadDelay;
	public int cur_gun;
	private float nextFire = 0.0F;

	private int cur_bullets;
	public int max_bullets;

	private List<int> gunlist;

	// Use this for initialization
	void Start (){
		gunlist = new List<int> ();
		cur_bullets = 0;
		gunlist.Add (cur_gun);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0) && Time.time > nextFire) {
			if ( cur_bullets > 0 )
			{
				cur_bullets--;
				nextFire = Time.time + fireRate;
				Vector3 mouse_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				Vector2 unit_direction = new Vector2 ((mouse_pos.x - this.transform.position.x), (mouse_pos.y - this.transform.position.y));
				unit_direction.Normalize();
				//Determines angle
				float angle = Mathf.Atan2 (mouse_pos.y - this.transform.position.y, mouse_pos.x - this.transform.position.x) * Mathf.Rad2Deg;

				if(cur_gun != 2)
				{
					Rigidbody2D bullet_instance = Instantiate (m_bullet, transform.position, Quaternion.Euler (new Vector3 (0, 0, angle - 90))) as Rigidbody2D;
					bullet_instance.velocity = (unit_direction * max_speed);
				}
				else
				{
					Rigidbody2D bullet_instance1 = Instantiate (m_bullet, transform.position, Quaternion.Euler (new Vector3 (0, 0, angle - 90))) as Rigidbody2D;
					Rigidbody2D bullet_instance2 = Instantiate (m_bullet, transform.position, Quaternion.Euler (new Vector3 (0, 0, angle - 90))) as Rigidbody2D;
					Rigidbody2D bullet_instance3 = Instantiate (m_bullet, transform.position, Quaternion.Euler (new Vector3 (0, 0, angle - 90))) as Rigidbody2D;
					Rigidbody2D bullet_instance4 = Instantiate (m_bullet, transform.position, Quaternion.Euler (new Vector3 (0, 0, angle - 90))) as Rigidbody2D;
					Rigidbody2D bullet_instance5 = Instantiate (m_bullet, transform.position, Quaternion.Euler (new Vector3 (0, 0, angle - 90))) as Rigidbody2D;

					Vector2 unit_direction2 = unit_direction;
					Vector2 unit_direction3 = unit_direction;
					Vector2 unit_direction4 = unit_direction;
					Vector2 unit_direction5 = unit_direction;

					unit_direction2 = Quaternion.Euler(0,0,5)*unit_direction2;
					unit_direction3 = Quaternion.Euler(0,0,-5)*unit_direction3;
					unit_direction4 = Quaternion.Euler(0,0,10)*unit_direction4;
					unit_direction5 = Quaternion.Euler(0,0,-10)*unit_direction5;

					bullet_instance1.velocity = (unit_direction * max_speed);
					bullet_instance2.velocity = (unit_direction2 * max_speed);
					bullet_instance3.velocity = (unit_direction3 * max_speed);
					bullet_instance4.velocity = (unit_direction4 * max_speed);
					bullet_instance5.velocity = (unit_direction5 * max_speed);
				}
			}
			else if ( cur_bullets <= 0 )
			{
				nextFire = Time.time + reloadDelay;
				cur_bullets = max_bullets;
			}
		}
		if (Input.GetKeyDown (KeyCode.R)) {
			nextFire = Time.time + reloadDelay;
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

				if ( cur_gun == 0 )
				{
					fireRate = 0.3F;
					reloadDelay = 0.4F;
					max_bullets = 6;
					cur_bullets = 6;
					nextFire = Time.time + reloadDelay;
				}
				else if ( cur_gun == 1)
				{
					fireRate = 0.7F;
					reloadDelay = 0.8F;
					max_bullets = 20;
					cur_bullets = 20;
					nextFire = Time.time + reloadDelay;
				}
				else if ( cur_gun == 2)
				{
					fireRate = 0.1F;
					reloadDelay = 1.5F;
					max_bullets = 20;
					cur_bullets	= 20;
					nextFire = Time.time + reloadDelay;
				}
			}
		}
		if (Input.GetKeyDown (KeyCode.E)) {
			if(gunlist.Count > 1)
			{
				if ( cur_gun == gunlist[gunlist.Count - 1] )
					cur_gun = gunlist[0]; 
				else
					cur_gun = gunlist[gunlist.IndexOf(cur_gun) + 1];     
			
				if ( cur_gun == 0 )
				{
					fireRate = 0.3F;
					reloadDelay = 0.4F;
					max_bullets = 6;
					cur_bullets = 6;
					nextFire = Time.time + reloadDelay;
				}
				else if ( cur_gun == 1)
				{
					fireRate = 0.7F;
					reloadDelay = 0.8F;
					max_bullets = 20;
					cur_bullets = 20;
					nextFire = Time.time + reloadDelay;
				}
				else if ( cur_gun == 2)
				{
					fireRate = 0.1F;
					reloadDelay = 1.5F;
					max_bullets = 20;
					cur_bullets	= 20;
					nextFire = Time.time + reloadDelay;
				}
			}
		}
	}
	public void AddGun (int i)
	{
		gunlist.Add (i);
	}

}
