using UnityEngine;
using System.Collections;

public class AttackingZombieScript : MonoBehaviour {

	public TextTimerScript m_timer_script;

	// Use this for initialization
	void Start () {
		this.GetComponent<SpriteRenderer> ().enabled = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (m_timer_script.GetTime () == 2000) {
			this.GetComponent<SpriteRenderer> ().enabled = true;
		}

		if (m_timer_script.GetTime() == 2100) {
			Destroy(gameObject);
		}
	}
}
