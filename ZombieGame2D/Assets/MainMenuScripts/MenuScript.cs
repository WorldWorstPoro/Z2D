using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour {

	public Canvas ExitMenu;
	public Button PlayText;
	public Button ExitText;
	public Button TutorialText;

	// Use this for initialization
	void Start () {
		ExitMenu = ExitMenu.GetComponent<Canvas> ();
		PlayText = PlayText.GetComponent<Button> ();
		ExitText = ExitText.GetComponent<Button> ();
		TutorialText = TutorialText.GetComponent<Button> ();

		ExitMenu.enabled = false;
	}
	
	public void ExitPress()
	{
		ExitMenu.enabled = true;
		PlayText.enabled = false;
		ExitText.enabled = false;
		TutorialText.enabled = false;
	}

	public void ExitNoPress()
	{
		ExitMenu.enabled = false;
		PlayText.enabled = true;
		ExitText.enabled = true;
		TutorialText.enabled = true;
	}

	public void ExitYesPress()
	{
		Application.Quit ();
	}

	public void PlayGame()
	{
		Application.LoadLevel (1);
	}

	public void PlayTutorial()
	{
		//Change this when we add play game, so that
		//the game scene is loaded as one.
		Application.LoadLevel (1);
	}


}
