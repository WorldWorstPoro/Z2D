using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MoneyTextScript : MonoBehaviour {

	private int m_money = 0;
	private Text m_text_component;

	// Use this for initialization
	void Start () {
		m_text_component = this.GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate(){
		m_text_component.text = m_money.ToString();
	}

	public void AddScore(int score)
	{
		m_money += score;
	}

	public void DecreaseScore(int score)
	{
		if (m_money - score > 0)
			m_money -= score;
	}
}
