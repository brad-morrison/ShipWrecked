using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsMain : MonoBehaviour
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
    	}
    }

    void leaderBoard()
    {
    	
    }

    void settings()
    {
    	PlayerPrefs.SetString("cam", "settings");
        SceneManager.LoadScene("Home");
    }

    void play()
    {
    	SceneManager.LoadScene("Main");
    }

    void customise()
    {
    	PlayerPrefs.SetString("cam", "outfits");
        SceneManager.LoadScene("Home");
    }
}
