using UnityEngine;
using System.Collections;

public class ShootBulletsScript : MonoBehaviour {

	public Rigidbody2D m_bullet;

	public float max_speed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Input.GetMouseButtonDown (0)) {
			Vector3 mouse_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			
			//Determines angle
			float angle = Mathf.Atan2 (mouse_pos.y - this.transform.position.y, mouse_pos.x - this.transform.position.x) * Mathf.Rad2Deg;
			
			Rigidbody2D bullet_instance = Instantiate (m_bullet, transform.position, Quaternion.Euler (new Vector3 (0, 0, angle - 90))) as Rigidbody2D;
			
			//Have a velocity of half the distance to the zombie
			Vector2 unit_direction = new Vector2 ((mouse_pos.x - this.transform.position.x), (mouse_pos.y - this.transform.position.y));
			unit_direction.Normalize();

			bullet_instance.velocity = (unit_direction * max_speed);
		}
	}
}
