using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUDScript : MonoBehaviour {

	
	public Button ExitBtn;
	// Use this for initialization
	void Start () {
		ExitBtn = ExitBtn.GetComponent<Button>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	
	public void ExitPress()
	{
		Application.LoadLevel(0);
	}
}
