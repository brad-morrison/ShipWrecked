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
        animator.SetBool("left", true);
        GetComponent<SpriteRenderer>().flipX = false;
    }

    public void moveRight()
    {
        animator.SetBool("right", true);
        GetComponent<SpriteRenderer>().flipX = true;
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
    }

    // called from animation when sword strikes
    void hit()
    {
        GM.playerHit();
    }
}