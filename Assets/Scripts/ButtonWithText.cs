using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonWithText : MonoBehaviour
{
    Buttons buttons;
    Audio audio;

    public Sprite off, on;

    public string buttonName;

    bool active;

    public bool audioTypeHome, audioTypeSettings;

    public GameObject text;
    Vector3 originalTextPos;

    // Start is called before the first frame update
    void Start()
    {
        buttons = GameObject.Find("scripts").GetComponent<Buttons>();
        audio = GameObject.Find("AUDIO").GetComponent<Audio>();
        originalTextPos = text.transform.position;
    }

    void OnMouseDown()
    {
        active = true;

    	GetComponent<SpriteRenderer>().sprite = on;
        text.transform.position = new Vector3 (text.transform.position.x, text.transform.position.y - 0.09f, text.transform.position.z);

        if (audioTypeHome)
        {
            audio.sound(audio.waterButton1, 1, 1);
        }
    }

    void OnMouseExit()
    {
        active = false;

    	GetComponent<SpriteRenderer>().sprite = off;
        text.transform.position = originalTextPos;

    }

    void OnMouseUp()
    {
    	GetComponent<SpriteRenderer>().sprite = off;
        text.transform.position = originalTextPos;

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

