using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpawnerScript : MonoBehaviour {

	public GameObject SpawnerNW;
	public GameObject SpawnerNE;
	public GameObject SpawnerSW;
	public GameObject SpawnerSE;
	public Rigidbody2D m_zombie;

	public int transition_one;
	public int transition_two;
	public int transition_three;
	public int transition_four;
	public int transition_five;

	private int spawning;
	private int timer;

	public int spawn_rate;
	public int zombie_speed;
	// Use this for initialization
	void Start () {
		spawning = 0;
		timer = 0;
	}
	
	// Update is called once per frame
	void Update () {
		spawning++;
		timer++;

		if (spawning >= spawn_rate && GameObject.Find ("Canvas").GetComponent<HUDScript>().EndText.GetComponent<GameOverScript>().dead != true) {
			spawning = 0;

			m_zombie.GetComponent<ZombieAvoidObstacleMovement>().max_speed = zombie_speed;

			int rand = Random.Range(0,4);

			switch(rand)
			{
			case 0:
				Rigidbody2D zombie_instanceNW = Instantiate (m_zombie, SpawnerNW.transform.position, Quaternion.Euler (new Vector3 (0, 0, 0))) as Rigidbody2D;
				break;
			case 1:
				Rigidbody2D zombie_instanceNE = Instantiate (m_zombie, SpawnerNE.transform.position, Quaternion.Euler (new Vector3 (0, 0, 0))) as Rigidbody2D;
				break;
			case 2:
				Rigidbody2D zombie_instanceSW = Instantiate (m_zombie, SpawnerSW.transform.position, Quaternion.Euler (new Vector3 (0, 0, 0))) as Rigidbody2D;
				break;
			case 3:
				Rigidbody2D zombie_instanceSE = Instantiate (m_zombie, SpawnerSE.transform.position, Quaternion.Euler (new Vector3 (0, 0, 0))) as Rigidbody2D;
				break;
			default:
				Rigidbody2D zombie_instanceNW2 = Instantiate (m_zombie, SpawnerNW.transform.position, Quaternion.Euler (new Vector3 (0, 0, 0))) as Rigidbody2D;
				break;
			}
		}

		if (timer == transition_one) {
			spawn_rate = 100;
		}

		if (timer == transition_two) {
			zombie_speed = 3;
		}

		if (timer == transition_three) {
			spawn_rate = 50;
		}

		if (timer == transition_four) {
			zombie_speed = 5;
		}

		if (timer == transition_five) {
			spawn_rate = 40;
			zombie_speed = 6;
		}
	}
}
