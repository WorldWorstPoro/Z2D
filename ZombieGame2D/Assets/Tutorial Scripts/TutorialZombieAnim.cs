using UnityEngine;
using System.Collections;

public class TutorialZombieAnim : MonoBehaviour {
	enum CURRENT_STATE {LEFT, RIGHT, UP, DOWN};

	CURRENT_STATE m_cur_state = CURRENT_STATE.LEFT;
	
	private int m_timer = -35;
	
	Rigidbody2D m_physics;
	public int m_run_speed;

	Animator m_animation;

	private BoxCollider2D m_collider;

	public MoneyTextScript money_text_script;
	
	// Use this for initialization
	void Start () {
		m_physics = this.GetComponent<Rigidbody2D>();
		m_physics.velocity = new Vector2 (-m_run_speed, 0);

		m_animation = this.GetComponent<Animator> ();
		m_animation.SetBool ("Walking", true);

		m_collider = this.GetComponent<BoxCollider2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void FixedUpdate(){
		++m_timer;

		if (m_timer < 1200) {

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
		}else if (m_timer == 1200) {
			//Stop zombie at time 1200
			m_physics.velocity = new Vector2(0,0);

			//Sets animation variable walking to false
			m_animation.SetBool("Walking", false);
		}
	}

	//Sets the animation state of the zombie to dead on collision with bullet
	void OnTriggerEnter2D (Collider2D col) 
	{
		if (col.tag == "Bullet") {
			m_animation.SetBool("Dead", true);
			money_text_script.AddScore(100);
		}
	}

}
