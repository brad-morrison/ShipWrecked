using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteButton : MonoBehaviour
{Buttons buttons;
    Audio audio;

    public Sprite muteOff_on, muteOff_off, muteOn_on, muteOn_off;

    bool active, muteOn;

    // Start is called before the first frame update
    void Start()
    {
        buttons = GameObject.Find("scripts").GetComponent<Buttons>();
        audio = GameObject.Find("AUDIO").GetComponent<Audio>();
    }

    void OnMouseDown()
    {
        active = true;

        if (!muteOn)
        {
            GetComponent<SpriteRenderer>().sprite = muteOff_on;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = muteOn_on;    
        }

        
        audio.sound(audio.waterButton1, 1, 1);
    }

    void OnMouseExit()
    {
        active = false;

        if (!muteOn)
        {
    	    GetComponent<SpriteRenderer>().sprite = muteOff_off;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = muteOn_off;
        }

    }

    void OnMouseUp()
    {
    	if (active)
        {
            if (!muteOn)
            {
                muteOn = true;
                buttons.muteOn();
                GetComponent<SpriteRenderer>().sprite = muteOn_off;
            }
            else
            {
                muteOn = false;
                buttons.muteOff();
                GetComponent<SpriteRenderer>().sprite = muteOff_off;
            }

            audio.sound(audio.waterButton2, 1, 1);

        }

        
        
    }


}
