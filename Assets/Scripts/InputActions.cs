using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputActions : MonoBehaviour
{
    GameManager GameManager_;
    AnimationEvents animationEvents;
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
        animationEvents = GameObject.Find("player").GetComponent<AnimationEvents>();
        GameManager_ = GameObject.Find("scripts").GetComponent<GameManager>();
    }

    public void TouchControls(string action)
    {
        GameManager_.playerTouched(action);
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("a") && inEditor)
        {
            //animationEvents.left();
            GameManager_.playerTouched("left");
        }

        if (Input.GetKeyDown("d") && inEditor)
        {
            //animationEvents.right();
            GameManager_.playerTouched("right");
        }
        
    }
}