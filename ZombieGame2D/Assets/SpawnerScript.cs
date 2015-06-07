using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpawnerScript : MonoBehaviour {

	public GameObject SpawnerNW;
	public GameObject SpawnerNE;
	public GameObject SpawnerSW;
	public GameObject SpawnerSE;
	public Rigidbody2D m_zombie;

	private int spawning;
	// Use this for initialization
	void Start () {
		spawning = 0;
	}
	
	// Update is called once per frame
	void Update () {
		spawning++;

		if (spawning == 100 && GameObject.Find ("Canvas").GetComponent<HUDScript>().EndText.GetComponent<GameOverScript>().dead != true) {
			spawning = 0;

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
				break;
			}
		}
	}
}
