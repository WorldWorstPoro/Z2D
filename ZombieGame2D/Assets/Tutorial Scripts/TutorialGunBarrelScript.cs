using UnityEngine;
using System.Collections;

/// <summary>
/// This is the point where the bullet is actually created.
/// </summary>
public class TutorialGunBarrelScript : MonoBehaviour {

	public Rigidbody2D m_bullet;		//Reference to the Prefab bullet					
	
	
	private TutorialPlayerAnimScript m_player_anim_script;				

	void Awake()
	{
		//Reference to the calling script so that we can use its timer variable
		m_player_anim_script = transform.root.GetComponent<TutorialPlayerAnimScript>();
	}

	
	// Update is called once per frame
	void FixedUpdate () {

		if (m_player_anim_script.m_timer == 1000 || m_player_anim_script.m_timer == 1050 || m_player_anim_script.m_timer == 1100 || m_player_anim_script.m_timer == 1350) {

			Vector3 zombieloc = GameObject.Find ("Tutorial Zombie- Walking").transform.position;
			var angle = Mathf.Atan2 (zombieloc.y - this.transform.position.y, zombieloc.x - this.transform.position.x) * Mathf.Rad2Deg;

			Rigidbody2D bullet_instance = Instantiate (m_bullet, transform.position, Quaternion.Euler (new Vector3 (0, 0, angle - 90))) as Rigidbody2D;
			
			//Have a velocity of half the distance to the zombie
			bullet_instance.velocity = new Vector2 ((zombieloc.x - this.transform.position.x) * 2, (zombieloc.y - this.transform.position.y) * 2);
		}
	
	}
}
