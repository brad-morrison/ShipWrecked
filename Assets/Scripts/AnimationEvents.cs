using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    Animator animate;
    GameManager GameManager_;
    public GameObject sharkStateLeft; 
    public GameObject shark;

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
            shark.transform.position = GameManager_.sharkLeft.transform.position; ///
            shark.transform.rotation = GameManager_.sharkLeft.transform.rotation;
            parent.transform.eulerAngles = new Vector3 (0, 0, 0);
            animate.SetBool("left", true);
            //GameManager_.playerTouched("left");
        }
    }

    public void right()
    {
        if (!animationOn)
        {
            shark.transform.position = GameManager_.sharkRight.transform.position; ///
            shark.transform.rotation = GameManager_.sharkRight.transform.rotation;
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

    public void hit()
    {
        GameManager_.bloodSpurt(shark.transform);
        shark.GetComponent<SpriteRenderer>().enabled = true;
        float pitchRand = Random.Range(0.90f, 1.1f);
        GameManager_.playSound(GameManager_.slice, 1, pitchRand);
        
        Instantiate(GameManager_.sharkDeathPrefab, shark.transform.position, shark.transform.rotation);
        GameManager_.sharkDiscLeft.transform.eulerAngles = new Vector3(0,180,0);
        GameManager_.sharkDiscRight.transform.eulerAngles = new Vector3(0,0,0);
        shark.GetComponent<SpriteRenderer>().enabled = false;
    }

    
}
