using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    Buttons buttons;
    Audio audio;

    public Sprite off, on;

    public string buttonName;

    bool active;

    public bool audioTypeHome;

    // Start is called before the first frame update
    void Start()
    {
        buttons = GameObject.Find("scripts").GetComponent<Buttons>();
        audio = GameObject.Find("AUDIO").GetComponent<Audio>();
    }

    void OnMouseDown()
    {
        active = true;

    	GetComponent<SpriteRenderer>().sprite = on;

        if (audioTypeHome)
        {
            audio.sound(audio.waterButton1, 1, 1);
        }
    }

    void OnMouseExit()
    {
        active = false;

    	GetComponent<SpriteRenderer>().sprite = off;

    }

    void OnMouseUp()
    {
    	GetComponent<SpriteRenderer>().sprite = off;

        if (active)
        {
            buttons.buttonPressed(buttonName);
        }

        if (audioTypeHome && active)
        {
            audio.sound(audio.waterButton2, 1, 1);
        }
    }


}
