using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextTimerScript : MonoBehaviour {
	
	private int m_timer = 0;
	private Text m_text_component;
	
	public int m_first_transition;
	public int m_second_transition;
	public int m_third_transition;
	public int m_fourth_transition;
	public int m_fifth_transition;
	public int m_sixth_transition;
	public int m_seventh_transition;
	public int m_eight_transition;
	public int m_ninth_transtion;
	public int m_tenth_transition;
	
	// Use this for initialization
	void Start () {
		m_text_component = this.GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public int GetTime(){
		return m_timer;
	}

	void FixedUpdate(){
		++m_timer;
		
		if (m_timer == m_first_transition) {
			m_text_component.text = "";
		} else if (m_timer == m_second_transition) {
			m_text_component.text = "Use the mouse to aim";
		} else if (m_timer == m_third_transition) {
			m_text_component.text = "";
		} else if (m_timer == m_fourth_transition) {
			m_text_component.text = "Left click on the screen\nTo shoot your weapon";
		} else if (m_timer == m_fifth_transition) {
			m_text_component.text = "Kill zombies to receive\nmoney to spend on upgrades";
		} else if (m_timer == m_sixth_transition) {
			m_text_component.text = "";
		} else if (m_timer == m_seventh_transition) {
			m_text_component.text = "Your score is determined\nby the amount of time you last";
		} else if (m_timer == m_eight_transition) {
			m_text_component.text = "";
		} else if (m_timer == m_ninth_transtion) {
			m_text_component.text = "Being hit by zombies lowers\n your health bar. Don't get hit!";
		} else if (m_timer == m_tenth_transition){
			m_text_component.text = "Walk over new weapons to\n Add them to your inventory! !";
		}
		
		
	}
}