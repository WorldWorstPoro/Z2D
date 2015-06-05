using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour {
	private Text m_text_component;
	public bool dead = false;

	// Use this for initialization
	void Start () {
		m_text_component = this.GetComponent<Text> ();	
	}
	
	// Update is called once per frame
	void Update () {
		if (dead) {
			GameObject timer = GameObject.Find("Timer");
			GameObject money = GameObject.Find("MoneyText");
			m_text_component.text = "Game Over!\nYou survived for: " + timer.GetComponent<Text>().text + 
				"\nAnd earned: $" + money.GetComponent<Text>().text;

		}
	}
}
