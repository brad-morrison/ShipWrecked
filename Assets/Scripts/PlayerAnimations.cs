using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    GameManager GM;
    Animator animator;
    
    public bool animationOn;

    void Start()
    {
        GM = GameObject.Find("scripts").GetComponent<GameManager>();
        animator = GetComponent<Animator>();
    }
    
    public void moveLeft()
    {
        if (!animationOn && !GM.playerDead)
        {
        animator.SetBool("left", true);
        animator.SetBool("right", false);
        transform.parent.transform.eulerAngles = new Vector3(0,0,0);
        }
    }

    public void moveRight()
    {
        if (!animationOn && !GM.playerDead)
        {
        animator.SetBool("right", true);
        animator.SetBool("left", false);
        transform.parent.transform.eulerAngles = new Vector3(0,180,0);
        }
    }

    // called from animation at start frame
    void animationStarted()
    {
        animationOn = true;
    }

    // called from animation at end frame
    void animationEnded()
    {
        animationOn = false;
        animator.SetBool("left", false);
        animator.SetBool("right", false);
    }

    // called from animation when sword strikes
    void hit()
    {
        GM.playerHit();
    }
}