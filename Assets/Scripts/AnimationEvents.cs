using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    Animator animate;
    GameManager GameManager_;
    public GameObject sharkStateLeft;

    public GameObject parent;
    
    public bool animationOn;
    
    // Start is called before the first frame update
    void Start()
    {
        animate = GetComponent<Animator>();
        GameManager_ = GameObject.Find("scripts").GetComponent<GameManager>();

    }

    public void left()
    {
        if (!animationOn)
        {
            parent.transform.eulerAngles = new Vector3 (0, 0, 0);
            animate.SetBool("left", true);
            //GameManager_.playerTouched("left");
        }
    }

    public void right()
    {
        if (!animationOn)
        {
            parent.transform.eulerAngles = new Vector3 (0, -180f, 0);
            animate.SetBool("right", true);
            //GameManager_.playerTouched("right");
        }
    }
    
    public void resetBool()
    {
        animate.SetBool("left", false);
        animate.SetBool("right", false);
        animationOn = false;
    }
    
    public void animationStarted()
    {
        animationOn = true;
    }

    
}
