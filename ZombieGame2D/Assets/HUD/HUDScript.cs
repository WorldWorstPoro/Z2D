using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUDScript : MonoBehaviour {

	public Slider HealthBar;
	public Button ExitBtn;
	public Text MoneyText;
	public Text TimerText;
	public Text EndText;
	public Image GunImage;
	public Image MoneyImage;

	public float Health = 1F;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	
	public void ExitPress()
	{
		Application.LoadLevel(0);
	}
}
