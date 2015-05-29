using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour {

	public TextTimerScript m_timer_script;
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (m_timer_script.GetTime () == 2100) {
			this.GetComponent<Slider>().value = this.GetComponent<Slider>().value - .1F;
		}
	}
}
