using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputActions : MonoBehaviour
{
    GameManager GameManager_;
    AnimationEvents animationEvents;
    
    // Start is called before the first frame update
    void Start()
    {
        animationEvents = GameObject.Find("player").GetComponent<AnimationEvents>();
        GameManager_ = GameObject.Find("scripts").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("a"))
        {
            //animationEvents.left();
            GameManager_.playerTouched("left");
        }

        if (Input.GetKeyDown("d"))
        {
            //animationEvents.right();
            GameManager_.playerTouched("right");
        }
    }
}