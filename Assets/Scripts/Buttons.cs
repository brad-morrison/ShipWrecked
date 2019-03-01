using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
	Audio audio;

	public bool muteMusic;
	void Start()
	{
		audio = GameObject.Find("AUDIO").GetComponent<Audio>();
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
    		case "mute":
    			mute();
    			break;

    		case "restore":
    			restore();
    			break;

    		case "language":
    			language();
    			break;

    		case "settingsToHomeArrow":
    			settingsToHomeArrow();
    			break;

    		// customise
    		case "nextOutfit":
    			nextOutfit();
    			break;

    		case "previousOutfit":
    			previousOutfit();
    			break;

    		case "chooseOutfit":
    			chooseOutfit();
    			break;

    	}
    }

    void leaderBoard()
    {
    	Debug.Log("leaderBoard pressed");
		// TEMP FOR TESTING
		if (muteMusic)
		{
			GameObject.Find("THEME").GetComponent<AudioSource>().mute = false;
			muteMusic = false;
		}
		else
		{
			GameObject.Find("THEME").GetComponent<AudioSource>().mute = true;
			muteMusic = true;
		}
		
    }

    void settings()
    {
    	Debug.Log("settings pressed");
    }

    void play()
    {
    	Debug.Log("play pressed");
		SceneManager.LoadScene("Main");
    }

    void customise()
    {
    	Debug.Log("customise pressed");
    }

    void mute()
    {
    	Debug.Log("mute pressed");
    }

    void restore()
    {
    	Debug.Log("restore pressed");
    }

    void language()
    {
    	Debug.Log("language pressed");
    }

    void settingsToHomeArrow()
    {
    	Debug.Log("arrow from settings to home");
    }

    void nextOutfit()
    {

    }

    void previousOutfit()
    {

    }

    void chooseOutfit()
    {
    	
    }

}
