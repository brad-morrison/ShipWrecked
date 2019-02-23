using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputActions : MonoBehaviour
{
    GameManager GM;
    public bool inEditor;
    
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
        GM.inputCheck(action);
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("a") && inEditor)
        {
            GM.inputCheck("left");
        }

        if (Input.GetKeyDown("d") && inEditor)
        {
            GM.inputCheck("right");
        }
        
    }
}