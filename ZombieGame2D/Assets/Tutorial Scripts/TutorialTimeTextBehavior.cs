using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TutorialTimeTextBehavior : MonoBehaviour {
	
	private int m_timer = 0;
	private int m_seconds = 0;
	private int m_minutes = 0;
	private int m_hours = 0;
	
	private Text m_text;
	
	private bool m_enabled = false;
	
	// Use this for initialization
	void Start () {
		m_text = this.GetComponent<Text> ();
		m_text.text = "";
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void FixedUpdate() {
		++m_timer;
		
		if ((m_timer % 50) == 0 ) {
			++m_seconds;
		}
		
		if (((m_seconds % 60) == 0) && (m_seconds > 0) ){
			m_seconds = 0;
			++m_minutes;
		}
		
		if (((m_minutes % 60) == 0) && (m_minutes > 0)){
			m_minutes = 0;
			++m_hours;
		}
		
		if (m_enabled == true) {
			
			string cur_time = "";
			
			if (m_hours > 0) {
				cur_time = m_hours.ToString () + ":";
			}
			
			cur_time = cur_time + m_minutes.ToString () + ":";
			
			if (m_seconds < 10) {
				cur_time += "0";
			}
			cur_time += m_seconds.ToString ();
			
			m_text.text = cur_time;
		}
		
		if (m_timer == 1650) {
			m_enabled = true;
		}
	}
}
