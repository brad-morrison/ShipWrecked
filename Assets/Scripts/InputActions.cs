using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputActions : MonoBehaviour
{
    GameManager GM;
    public bool inEditor, octopusActive, pufferfishActive;
    
    void Awake()
    {
        if (Application.isEditor)
        {
            inEditor = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.Find("scripts").GetComponent<GameManager>();
    }

    public void TouchControls(string action)
    {
        if (octopusActive)
        {
            GM.octopusInteractionCheck(action);
        }
		else if (pufferfishActive)
        {
            GM.pufferfishInteractionCheck(action);
        }
        else
        {
        	GM.inputCheck(action);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("a") && inEditor)
        {
            if (octopusActive)
            {
                GM.octopusInteractionCheck("left");
            }
            else if (pufferfishActive)
            {
                GM.pufferfishInteractionCheck("left");
            }
            else
            {
                GM.inputCheck("left");
            }
        }

        if (Input.GetKeyDown("d") && inEditor)
        {
            if (octopusActive)
            {
                GM.octopusInteractionCheck("right");
            }
            else if (pufferfishActive)
            {
                GM.pufferfishInteractionCheck("right");
            }
            else
            {
                GM.inputCheck("right");
            }
        }
        
    }
}