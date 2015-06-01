using UnityEngine;
using System.Collections;

public class PlayerGunMotion : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void LateUpdate () {
		Vector3 mouse_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		
		//Determines angle
		float angle = Mathf.Atan2 (mouse_pos.y - this.transform.position.y, mouse_pos.x - this.transform.position.x) * Mathf.Rad2Deg;

		//Angle - 90 has to be used because the player sprite is pointed upward
		transform.rotation = Quaternion.Euler (new Vector3 (0, 0, angle - 90));
	}
}
