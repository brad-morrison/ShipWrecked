using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
	Audio audio;
	CameraMenuMovement camMove;
	ChooseOutfit changeOutfit;

	public GameObject gameByText, name_on, name_off;

	void Start()
	{
		audio = GameObject.Find("AUDIO").GetComponent<Audio>();
		camMove = GameObject.Find("Main Camera").GetComponent<CameraMenuMovement>();
		changeOutfit = GameObject.Find("scripts").GetComponent<ChooseOutfit>();
	}

	public void buttonPressed(string buttonName)
    {
    	switch (buttonName)
    	{
    		// home
    		case "leaderboards":
    			leaderBoard();
    			break;

    		case "settings":
    			settings();
    			break;

    		case "play":
    			play();
    			break;

    		case "customise":
    			customise();
    			break;

    		// settings
    		case "restore":
    			restore();
    			break;

    		case "language":
    			language();
    			break;

				case "removeAds":
    			removeAds();
    			break;

    		case "settingsToHomeArrow":
    			settingsToHomeArrow();
    			break;

    		// customise
    		case "nextOutfit":
    			nextOutfit();
    			break;

    		case "prevOutfit":
    			prevOutfit();
    			break;

    		case "chooseOutfit":
    			chooseOutfit();
    			break;

    	}
    }

    void leaderBoard()
    {
    	Debug.Log("leaderBoard pressed");
    }

    void settings()
    {
    	Debug.Log("settings pressed");
			camMove.move(camMove.targetSettings);
			StartCoroutine(activateName(0.3f));
    }

    void play()
    {
    	Debug.Log("play pressed");
			SceneManager.LoadScene("Main");
    }

    void customise()
    {
    	Debug.Log("customise pressed");
			camMove.move(camMove.targetOutfits);
    }

    public void muteOn()
    {
    	Debug.Log("mute on");
			GameObject.Find("THEME").GetComponent<AudioSource>().mute = true;
    }

		public void muteOff()
    {
    	Debug.Log("mute off");
			GameObject.Find("THEME").GetComponent<AudioSource>().mute = false;
    }

    void restore()
    {
    	Debug.Log("restore pressed");
    }

		void removeAds()
		{

		}

    void language()
    {
    	Debug.Log("language pressed");
    }

    void settingsToHomeArrow()
    {
    	Debug.Log("arrow from settings to home");
			camMove.move(camMove.targetMain);
			StartCoroutine(deactivateName(0.1f));
    }

    void nextOutfit()
    {
			changeOutfit.nextOutfit();
    }

    void prevOutfit()
    {
			changeOutfit.prevOutfit();
    }

    void chooseOutfit()
    {
    	changeOutfit.choose();
			camMove.move(camMove.targetMain);
    }

		IEnumerator activateName(float wait)
		{
			yield return new WaitForSeconds(wait);
			name_off.active = false;
			name_on.active = true;
			gameByText.GetComponent<MeshRenderer>().enabled = true;
		}

		IEnumerator deactivateName(float wait)
		{
			yield return new WaitForSeconds(wait);
			name_off.active = true;
			name_on.active = false;
			gameByText.GetComponent<MeshRenderer>().enabled = false;
		}

}
