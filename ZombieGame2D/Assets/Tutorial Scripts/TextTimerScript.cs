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
	//Decided to make these last ones private to avoid having issues with the values set in the editor not syncing.
	private int m_eleventh_transition = 2700;
	private int m_twelfth_transition = 2900;
	private int m_thirteenth_transition = 3200;
	private int m_fourteenth_transition = 3600;
	private int last_transition = 3700;

	public Text m_money_text;
	public Image m_money_image;

	public Slider m_health_bar;

	public Text m_ammo_text;

	// Use this for initialization
	void Start () {
		m_text_component = this.GetComponent<Text> ();

		m_money_text.enabled = false;
		m_money_image.enabled = false;

		foreach (Image image in m_health_bar.GetComponentsInChildren<Image>()) {
			image.enabled = false;
		}
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
			m_money_image.enabled = true;
			m_money_text.enabled = true;
		} else if (m_timer == m_sixth_transition) {
			m_text_component.text = "";
		} else if (m_timer == m_seventh_transition) {
			m_text_component.text = "Your score is determined\nby the amount of time you last";
		} else if (m_timer == m_eight_transition) {
			m_text_component.text = "";
		} else if (m_timer == m_ninth_transtion) {
			m_text_component.text = "Being hit by zombies lowers\n your health bar. Don't get hit!";
			foreach (Image image in m_health_bar.GetComponentsInChildren<Image>()) {
				image.enabled = true;
			}
		} else if (m_timer == m_tenth_transition) {
			m_text_component.text = "You can use the R key to\nreload your weapon. You will also\nreload automatically when you are out of ammo.";
			m_ammo_text.text = "reloading...";
		} else if (m_timer == m_eleventh_transition) {
			m_text_component.text = "";
			m_ammo_text.text = "6/6";
		} else if (m_timer == m_twelfth_transition) {
			m_text_component.text = "Walk over new weapons to\n Add them to your inventory!\n(Provided you have enough money for them)";
		} else if (m_timer == m_thirteenth_transition) {
			m_text_component.text = "You can then swap between weapons\nby using the E and Q buttons";
		} else if (m_timer == m_fourteenth_transition) {
			m_text_component.text = "That should be everything you need.\nGood luck player!";
		} else if (m_timer == last_transition) {
			Application.LoadLevel(0);
		}
		
		
	}
}