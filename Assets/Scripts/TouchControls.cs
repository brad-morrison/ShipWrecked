using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchControls : MonoBehaviour
{
    InputActions InputActions_s;
    public string action;
    
    // Start is called before the first frame update
    void Start()
    {
        InputActions_s = GameObject.Find("scripts").GetComponent<InputActions>();

        if (InputActions_s.inEditor)
        {
            GetComponent<BoxCollider>().enabled = false;
        }
    }

    void OnMouseDown()
    {
        InputActions_s.TouchControls(action);
    }
}
