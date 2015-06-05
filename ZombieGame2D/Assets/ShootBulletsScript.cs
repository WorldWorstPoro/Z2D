using UnityEngine;
using System.Collections;

public class ShootBulletsScript : MonoBehaviour {

	public Rigidbody2D m_bullet;

	public float max_speed;

	public float fireRate;
	public float reloadDelay;
	public int cur_gun;
	private float nextFire = 0.0F;
	public enum guns {PISTOL = 0, SHOTGUN};

	private int cur_bullets;
	public int max_bullets;

	// Use this for initialization
	void Start (){
		cur_bullets = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0) && Time.time > nextFire) {
			if ( cur_bullets > 0 )
			{
				cur_bullets--;
				nextFire = Time.time + fireRate;
				Vector3 mouse_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				
				//Determines angle
				float angle = Mathf.Atan2 (mouse_pos.y - this.transform.position.y, mouse_pos.x - this.transform.position.x) * Mathf.Rad2Deg;
				
				Rigidbody2D bullet_instance = Instantiate (m_bullet, transform.position, Quaternion.Euler (new Vector3 (0, 0, angle - 90))) as Rigidbody2D;
				
				//Have a velocity of half the distance to the zombie
				Vector2 unit_direction = new Vector2 ((mouse_pos.x - this.transform.position.x), (mouse_pos.y - this.transform.position.y));
				unit_direction.Normalize();

				bullet_instance.velocity = (unit_direction * max_speed);
			}
			else if ( cur_bullets <= 0 )
			{
				nextFire = Time.time + reloadDelay;
				cur_bullets = max_bullets;
			}
		}
		if (Input.GetKey (KeyCode.R)) {
			nextFire = Time.time + reloadDelay;
		}
	}
}
