using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkState : MonoBehaviour
{
    // base state
    // base state with mouth slowly opening
    // mouth open state
    // shark win state
    //        or
    // shark death state

    GameManager GameManager_;
    
    public int currentState;

    float stateOne, stateTwo, stateThree, stateFour, stateFive;

    public GameObject player, sharkBase, sharkMouth, sharkOpen, sharkDeath;
    

    public bool dead, lose;

    public bool left, rotate;
    
    // Start is called before the first frame update
    void Start()
    {
        GameManager_ = GameObject.Find("scripts").GetComponent<GameManager>();
        player = GameObject.Find("player");

        stateOne = 43f;    // base state limit
        stateTwo = 65f;    // start mouth opening state
        stateThree = 70f;   // start mouth open state
        //stateFour = 80f;    // lose state
    }

    void hideAll()
    {
        sharkBase.active = false;
        sharkOpen.active = false;
        sharkDeath.active = false;
    }
    
    void StateOne()
    {
        hideAll();
        sharkBase.active = true;
    }

    void StateTwo()
    {
        hideAll();
        sharkBase.active = true;
        // begin moving mouth;
        if (left)
        {
            sharkMouth.transform.eulerAngles = new Vector3(0,0,-(transform.eulerAngles.z - 63) * 2.5f);
        }

        if (!left)
        {
            sharkMouth.transform.eulerAngles = new Vector3(0,0,(transform.eulerAngles.z + 18) * 2.5f);
        }
    }

    void StateThree()
    {
        hideAll();
        sharkOpen.active = true;
    }
    
    public void StateFour()
    {
        if (!dead)
        {
            hideAll();
             // changes angle of animation for variation
            sharkDeath.active = true;
        }
        dead = true;
    }

    public void StateFive()
    {
        //lose state
    }
    
    // Update is called once per frame
    void Update()
    {
        if (!dead && !lose)
        {
            if (transform.localEulerAngles.z <= stateOne && transform.localEulerAngles.z >= 0)
            {
                currentState = 1;
                StateOne();
            }

            if (transform.eulerAngles.z <= stateTwo && transform.eulerAngles.z >= stateOne)
            {
                currentState = 2;
                StateTwo();
            }

            if (transform.eulerAngles.z <= stateThree && transform.eulerAngles.z >= stateTwo)
            {
                currentState = 3;
                StateThree();
            }

            // state four is inititiated by the player (shark death)
            
            if (transform.eulerAngles.z > 75)
            {
                currentState = 5;
                StateFive();
            }
        }

        if (lose)
        {
            currentState = 5;
            StateFive();
        }

        if (rotate)
        {
            if (currentState == 1)
            {
                transform.Rotate(new Vector3(0,0,1), Time.deltaTime * GameManager_.waterBreakSpeed);
            }

            if (currentState == 2)
            {
                transform.Rotate(new Vector3(0,0,1), Time.deltaTime * GameManager_.difficultySpeed);
            }

            if (currentState == 3)
            {
                transform.Rotate(new Vector3(0,0,1), Time.deltaTime * GameManager_.difficultySpeed);
            }

            if (currentState == 5)
            {
                transform.Rotate(new Vector3(0,0,1), Time.deltaTime * GameManager_.eatPlayerSpeed);

                if (transform.eulerAngles.z > 103f && transform.eulerAngles.z < 250f)
                {
                    player.transform.SetParent(sharkBase.transform);
                }
                
                if (transform.eulerAngles.z >= 250)
                {
                    currentState = 0;
                    rotate = false;
                    GameManager_.GameOver();
                }
            }
        }
    }
}
