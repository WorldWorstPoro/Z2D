using UnityEngine;
using System.Collections;

public class ZombieAvoidObstacleMovement : MonoBehaviour {

	private Rigidbody2D m_physics;
	public int avoid_layer;
	public float max_speed;

	private WayPointHandler m_waypoint_handle;

	private GameObject player;

	// Use this for initialization
	void Start () {
		m_physics = this.GetComponent<Rigidbody2D> ();
		player  = GameObject.Find ("Player");

		m_physics.velocity = new Vector2 (player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y).normalized * max_speed;

		m_waypoint_handle = GameObject.Find ("WayPoints").GetComponent<WayPointHandler> ();
	}
	
	// Update is called once per frame
	void Update () {

		//Handles the rotation of the zombie
		Vector2 moveDirection = m_physics.velocity;

		if (moveDirection != Vector2.zero) {
			float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Euler (new Vector3 (0, 0, angle + 90));
		}
	}

	void FixedUpdate(){
		RaycastHit2D hit;
		
		//Only detect the set layer
		int layer_mask = 1<< avoid_layer;

		Vector2 dir_to_player = new Vector2 (player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y);

		float max_distance = dir_to_player.magnitude;
		float min_distance_check = dir_to_player.sqrMagnitude;

		dir_to_player.Normalize ();

		//If the distance to the player is far enough, try to find a path to him
		//This distance is approximately where the zombie makes contact with the player
		if (min_distance_check > 1) {

			hit = Physics2D.Raycast(transform.position, dir_to_player, max_distance, layer_mask);


			//If there is an object in the set layer between the zombie and the player, move at an angle from it.
			if (hit.collider != null ) {
				dir_to_player = (Vector2)m_waypoint_handle.GetNextWayPointLocation(transform.position) - (Vector2)transform.position;
				m_physics.velocity = dir_to_player.normalized * max_speed;
			} 
			//Otherwise just keep moving towards the player
			else {
				m_physics.velocity = dir_to_player * max_speed;
			}
		} else {
			//Start attack animation?
		}
	}
}
