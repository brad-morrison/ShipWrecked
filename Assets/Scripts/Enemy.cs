using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    GameManager GM;
    
    public string enemyType;
    bool rotate = true;

    public GameObject currentEnemy;
    
    // shark prefabs and sprites
    public GameObject shark, sharkDeathAnimation;
    public Sprite sharkStay, sharkEnd;

    // octopus prefabs and sprites
    public GameObject octopus, octopusDeathAnimation;
    public Sprite octopusEnd;

    // phase rotations
    int currentPhase;
    float phaseOneLimit = 43f;       // fast rotation at beginning
    float phaseTwoLimit = 65f;       // main difficulty rotation
    float phaseThreeLimit = 70f;     // final stage before death

    bool playerDead = false; // used to stop rotation update from taking over the death movement

    void Start()
    {
        GM = GameObject.Find("scripts").GetComponent<GameManager>();
    }
    
    public void stopRotation()
    {
        rotate = false;
    }

    public void killPlayer()
    {
        playerDead = true;
        currentPhase = 3;
    }

    public void sharkDeath()
    {
        rotate = false;
        
        // death animation
        if (GM.leftActive)
        {
            GameObject sharkTemp = Instantiate(sharkDeathAnimation, currentEnemy.transform.position, currentEnemy.transform.rotation);
        }
        else
        {
            GameObject sharkTemp = Instantiate(sharkDeathAnimation, currentEnemy.transform.position, currentEnemy.transform.rotation);
            sharkTemp.transform.eulerAngles = new Vector3(0, 180, 0);
        }

        GM.bloodSpurt(currentEnemy.transform);
        
        Destroy(gameObject);
    }

    public void octopusDeath()
    {
        rotate = false;

        // death animation
        if (GM.leftActive)
        {
            GameObject octoTemp = Instantiate(octopusDeathAnimation, currentEnemy.transform.position, currentEnemy.transform.rotation);
        }
        else
        {
            GameObject sharkTemp = Instantiate(octopusDeathAnimation, currentEnemy.transform.position, currentEnemy.transform.rotation);
            sharkTemp.transform.eulerAngles = new Vector3(0, 180, 0);
        }

        GM.bloodSpurt(currentEnemy.transform);
        
        Destroy(gameObject);
    }

    void phaseOne()
    {
        currentPhase = 1;

        // if I need to add phaseOne sprites later
    }

    void phaseTwo()
    {
        currentPhase = 2;

        if (enemyType == "shark")
        {
            currentEnemy.GetComponent<SpriteRenderer>().sprite = sharkStay;
        }

        if (enemyType == "octopus")
        {
            currentEnemy.GetComponent<SpriteRenderer>().sprite = octopusEnd;
        }
    }

    void phaseThree()
    {
        currentPhase = 3;

        if (enemyType == "shark")
        {
            currentEnemy.GetComponent<SpriteRenderer>().sprite = sharkEnd;
        }

        if (enemyType == "octopus")
        {
            currentEnemy.GetComponent<SpriteRenderer>().sprite = octopusEnd;
        }
    }
    
    void Update()
    {
        if (!playerDead)
        {
            // phase control
            if (transform.localEulerAngles.z <= phaseOneLimit && transform.localEulerAngles.z >= 0)
            {
                phaseOne();
            }

            if (transform.eulerAngles.z <= phaseTwoLimit && transform.eulerAngles.z >= phaseOneLimit)
            {
                phaseTwo();
            }

            if (transform.eulerAngles.z <= phaseThreeLimit && transform.eulerAngles.z >= phaseTwoLimit)
            {
                phaseThree();
            }
        }

        // rotating disc over time
        if (rotate)
        {
            if (currentPhase == 1)
            {
                transform.Rotate(new Vector3(0,0,1), Time.deltaTime * GM.waterBreakSpeed);
            }

            if (currentPhase == 2)
            {
                transform.Rotate(new Vector3(0,0,1), Time.deltaTime * GM.difficultySpeed);
            }

            if (currentPhase == 3)
            {
                transform.Rotate(new Vector3(0,0,1), Time.deltaTime * GM.eatPlayerSpeed);

                // wait till rotation = player position
                if (transform.eulerAngles.z > 103f && transform.eulerAngles.z < 250f)
                {
                    GM.player.transform.SetParent(this.transform);
                }

                // when in water, game over
                if (transform.eulerAngles.z >= 250)
                {
                    rotate = false;
                    GM.gameOver();
                }
            }

        }
    }
}