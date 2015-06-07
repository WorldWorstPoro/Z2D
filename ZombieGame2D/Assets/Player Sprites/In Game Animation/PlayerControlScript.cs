using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerControlScript : MonoBehaviour {

	private Rigidbody2D m_physics;
	private Animator m_animator;

	public float max_speed;

	private int m_cur_motion;
	private int health = 100;
	private int regen = 0;

	// Use this for initialization
	void Start () {
		m_physics = GetComponent<Rigidbody2D> ();
		m_animator = GetComponent<Animator> ();

		m_animator.SetInteger ("MotionType", 0);

		m_cur_motion = 0;

	}

	void Update(){
		Vector3 mouse_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		
		//Determines angle
		float angle = Mathf.Atan2 (mouse_pos.y - this.transform.position.y, mouse_pos.x - this.transform.position.x) * Mathf.Rad2Deg;

		//Gets the angle in 45 degree increments, sector corresponds to which 45 degree segment the player is facing
		int sector = (int)(Mathf.Floor ((angle + 22.5F) / 45));

		//Creates a new angle to use for the rotation so the original angle can be used for the gun barrel.
		angle = sector * 45;

		//Angle - 90 has to be used because the player sprite is pointed upward
		transform.rotation = Quaternion.Euler (new Vector3 (0, 0, angle - 90));

		//Rotates the gun barrel separately to avoid it not pointing directly at the target.
		//m_gun_barrel.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, angle - 90));

		//Holds the value for the current animation that should be played based on the orientation
		int cur_anim_motion = 0;

		//If the player is facing directly up or down, no change needs to be made.
		//0 corresponds to still and should not need to change
		//1 cooresponds to forward
		//2 corresponds to sideways
		//3 corresponds to backward right to forward left
		//4 corresponds to backward left to forward right

		//Up and dowward sectors
		if (sector == 2 || sector == -2) {
			cur_anim_motion = m_cur_motion;
		}//Left and Right sectors 
		else if (sector == 0 || sector == -4 || sector == 4) {
			switch (m_cur_motion){
			case 1:
				cur_anim_motion = 2;
				break;
			case 2:
				cur_anim_motion = 1;
				break;
			case 3: 
				cur_anim_motion = 4;
				break;
			case 4:
				cur_anim_motion = 3;
				break;
			default:
				cur_anim_motion = 0;
				break;
			}
		}//Upward right and downward left sectors 
		else if (sector == 1 || sector == -3) {
			switch (m_cur_motion){
			case 1:
				cur_anim_motion = 3;
				break;
			case 2:
				cur_anim_motion = 4;
				break;
			case 3: 
				cur_anim_motion = 2;
				break;
			case 4:
				cur_anim_motion = 1;
				break;
			default:
				cur_anim_motion = 0;
				break;
			}
		}//Upward left and downward right sectors
		else if (sector == 3 || sector == -1) {
			switch (m_cur_motion){
			case 1:
				cur_anim_motion = 4;
				break;
			case 2:
				cur_anim_motion = 3;
				break;
			case 3: 
				cur_anim_motion = 1;
				break;
			case 4:
				cur_anim_motion = 2;
				break;
			default:
				cur_anim_motion = 0;
				break;
			}
		}
		//Update the animation as needed.
		m_animator.SetInteger ("MotionType", cur_anim_motion);
	}

	// FixedUpdate is called once per physics step
	void FixedUpdate () {

		regen++;

		if ((regen % 100 == 0) && (health < 100)) {
			regen = 0;
			health++;

			GameObject gis = GameObject.Find ("Canvas");
			gis.GetComponent<HUDScript> ().HealthBar.value = gis.GetComponent<HUDScript> ().HealthBar.value + .01F;

		}


		if (m_animator != null) {

			int horizontal_axis = 0;
			int vertical_axis = 0;

			if (Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.LeftArrow)) {
				horizontal_axis -= 1;
			}

			if (Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.RightArrow)) {
				horizontal_axis += 1;
			}

			if (Input.GetKey (KeyCode.S) || Input.GetKey (KeyCode.DownArrow)) {
				vertical_axis -= 1;
			}

			if (Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.UpArrow)) {
				vertical_axis += 1;
			}

			//If neither axis is pressed, do not move
			if ((Mathf.Abs (horizontal_axis) + Mathf.Abs (vertical_axis)) == 0) {

				m_cur_motion = 0;
				m_physics.velocity = new Vector2 (0, 0);
			} else {
				//If an axis is pressed create a velocity of max_speed in the direction(s) pressed
				Vector2 new_velocity = new Vector2 (horizontal_axis, vertical_axis);

				new_velocity.Normalize ();

				m_physics.velocity = new_velocity * max_speed;

				//Updates the animation that is currently playing
				if (horizontal_axis == 0) {
					m_cur_motion = 1;
				} else if (vertical_axis == 0) {
					m_cur_motion = 2;
				} else if (((vertical_axis > 0) && (horizontal_axis > 0)) || ((vertical_axis < 0) && (horizontal_axis < 0))) {
					m_cur_motion = 4;
				} else {
					m_cur_motion = 3;
				}
			}
		}
	}

	void OnCollisionEnter2D(Collision2D col){
			
			if (col.collider.tag == "Zombie") {
			GameObject gis = GameObject.Find ("Canvas");
			gis.GetComponent<HUDScript> ().HealthBar.value = gis.GetComponent<HUDScript> ().HealthBar.value - .1F;
			health -= 10;	
		
			if (health <= 0) {
				GameObject ply = GameObject.Find ("Player");
				ply.SetActive (false);
				gis.GetComponent<HUDScript> ().TimerText.GetComponent<TimeTextBehavior> ().m_enabled = false;
				gis.GetComponent<HUDScript> ().EndText.GetComponent<GameOverScript> ().dead = true;
			}
		} else if (col.collider.tag == "Gun") {
			//TO DO: replace arbitrary gun prices with gun price in UI
			//		 Destroy gun price object from the UI
			GameObject gun = GameObject.Find ("GunBarrel");
			GameObject canvas = GameObject.Find ("Canvas");
			string smoney = canvas.GetComponent<HUDScript> ().MoneyText.text;
			int imoney = int.Parse (smoney);

			bool enoughMoney = false;
				
			if (col.gameObject.name == "Shotgun") {
				int sPrice = int.Parse (GameObject.Find ("Z2Dcost2").GetComponent<Text>().text);
				if (imoney >= sPrice)
				{
					gun.GetComponent<ShootBulletsScript> ().AddGun (2);
					imoney -= sPrice;
					enoughMoney = true;
					Destroy (GameObject.Find ("Z2Dcost2"));
				}
			} else if (col.gameObject.name == "Machinegun") {
				int mPrice = int.Parse (GameObject.Find ("Z2Dcost1").GetComponent<Text>().text);
				if(imoney >= mPrice)
				{
					gun.GetComponent<ShootBulletsScript> ().AddGun (1);
					imoney -= mPrice;	
					enoughMoney = true;
					Destroy (GameObject.Find ("Z2Dcost1"));
				}
			}
			if (enoughMoney) {
				smoney = imoney.ToString ();
				canvas.GetComponent<HUDScript> ().MoneyText.text = smoney;
				Destroy (col.gameObject);
			}
		}
	}
}
