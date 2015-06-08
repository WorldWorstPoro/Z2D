using UnityEngine;
using System.Collections;

public class TutorialPlayerAnimScript : MonoBehaviour {
	
	enum CURRENT_STATE {LEFT, RIGHT, UP, DOWN};
	private bool m_running = true;
	
	CURRENT_STATE m_cur_state = CURRENT_STATE.LEFT;
	
	public int m_timer = 0;
	
	Rigidbody2D m_physics;
	Rigidbody2D m_bullet_physics;
	public int m_run_speed;
	
	private Animator m_animation;
	
	// Use this for initialization
	void Start () {
		m_physics = this.GetComponent<Rigidbody2D>();
		m_physics.velocity = new Vector2 (-m_run_speed, 0);
		m_animation = this.GetComponent<Animator> ();
		m_animation.SetBool("Standing", false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void FixedUpdate(){
		++m_timer;
		
		if (m_timer < 680) {
			if (m_timer % 120 == 0 && m_timer > 0) {
				switch (m_cur_state) {
				case CURRENT_STATE.LEFT:
					m_cur_state = CURRENT_STATE.DOWN;
					m_timer += 20;
					m_physics.velocity = new Vector2 (0, -m_run_speed);
					break;
				case CURRENT_STATE.DOWN:
					m_cur_state = CURRENT_STATE.RIGHT;
					m_physics.velocity = new Vector2 (m_run_speed, 0);
					break;
				case CURRENT_STATE.RIGHT:
					m_cur_state = CURRENT_STATE.UP;
					m_timer += 20;
					m_physics.velocity = new Vector2 (0, m_run_speed);
					break;
				case CURRENT_STATE.UP:
					m_cur_state = CURRENT_STATE.LEFT;
					m_physics.velocity = new Vector2 (-m_run_speed, 0);
					break;
				}
				
				transform.Rotate (Vector3.forward * 90);
			}
		}
		
		if (m_timer == 680) {
			m_cur_state = CURRENT_STATE.RIGHT;
			transform.Rotate (Vector3.forward * 90);
			m_physics.velocity = new Vector2 (m_run_speed, 0);
		}
		
		if (m_timer == 740) {
			m_running = false;
			m_physics.velocity = new Vector2 (0, 0);
			m_animation.enabled = false;
		}

		if (m_running == false && m_timer < 1300) {
			Vector3 zombieloc = GameObject.Find ("Tutorial Zombie- Walking").transform.position;
			m_animation.SetBool("Standing", true);
			//Determines angle
			var angle = Mathf.Atan2 (zombieloc.y - this.transform.position.y, zombieloc.x - this.transform.position.x) * Mathf.Rad2Deg;

			//Angle - 90 has to be used because the player sprite is pointed upward
			transform.rotation = Quaternion.Euler (new Vector3 (0, 0, angle - 90));
		}

		if (m_timer == 2850) {
			m_running = true;
			m_animation.enabled = true;
			transform.Rotate (Vector3.forward * 150);

			m_physics.velocity = new Vector2 (m_run_speed, 0);
			m_animation.SetBool("Standing", false);
		}
	}

		void OnTriggerEnter2D (Collider2D col) 
		{
			if (col.tag == "Gun") {
				m_physics.velocity = new Vector2(0,0);
				m_animation.enabled = false;
				m_animation.SetBool("Standing", true);
			}
		}
}
