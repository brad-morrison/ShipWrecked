using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shark : MonoBehaviour
{
    GameManager GM;
    GameObject camera;
    Spawner spawn;
    
    public string enemyType;
    public bool rotate = true;
    Audio audio;

    public GameObject currentEnemy;
    
    // shark prefabs and sprites
    public GameObject sharkDeathAnimation;
    public Sprite sharkStay, sharkEnd;

    // phase rotations
    public int currentPhase;
    float phaseOneLimit = 55f;       // fast rotation at beginning
    float phaseTwoLimit = 78f;       // main difficulty rotation
    float phaseThreeLimit = 85f;     // final stage before death
    float phaseFourLimit = 93f;     // death

    bool playerDead = false; // used to stop rotation update from taking over the death movement
    bool deadAnimationsPlayed = false;
    bool splashHitPlayed;

    void Start()
    {
        GM = GameObject.Find("scripts").GetComponent<GameManager>();
        GM.enemyType = enemyType;
        spawn = GameObject.Find("scripts").GetComponent<Spawner>();
        GM.Shark_s = this;
        camera = GameObject.Find("Main Camera");
        audio = GameObject.Find("AUDIO").GetComponent<Audio>();
    }
    
    public void stopRotation()
    {
        rotate = false;
    }

    public void killPlayer()
    {
        playerDead = true;
        currentPhase = 4;
    }

    public void sharkDeath()
    {
        GM.sharkCount = GM.sharkCount + 1;
        rotate = false;
        GameObject sharkTemp;
        
        // death animation
        if (GM.leftActive)
        {
            sharkTemp = Instantiate(sharkDeathAnimation, currentEnemy.transform.position, currentEnemy.transform.rotation);
        }
        else
        {
            sharkTemp = Instantiate(sharkDeathAnimation, currentEnemy.transform.position, currentEnemy.transform.rotation);
            sharkTemp.transform.eulerAngles = new Vector3(0, 180, 0);
        }

        GM.bloodSpurt(currentEnemy.transform);
        
        currentEnemy.GetComponent<SpriteRenderer>().enabled = false;
        StartCoroutine(destroyAfter(sharkTemp, 1f));
        StartCoroutine(destroyAfter(gameObject, 1f));
    }

    void phaseOne()
    {
        currentPhase = 1;

        // if I need to add phaseOne sprites later
    }

    void phaseTwo()
    {
        currentPhase = 2;

        currentEnemy.GetComponent<SpriteRenderer>().sprite = sharkStay;
    }

    void phaseThree()
    {
        currentPhase = 3;

        currentEnemy.GetComponent<SpriteRenderer>().sprite = sharkEnd;
    }

    void phaseFour()
    {
        currentPhase = 4;
        GM.playerDead = true;
        currentEnemy.GetComponent<SpriteRenderer>().sprite = sharkEnd;
    }

    void playerDeadAnimations()
    {
        if (!deadAnimationsPlayed)
        {
            GM.camera.GetComponent<CamShake>().shake(0.2f);
            GM.bloodSpurt(GM.player.transform.parent.transform, 2);
            audio.sound(audio.slice, 1, 1);
            GM.player.transform.SetParent(this.transform);
            deadAnimationsPlayed = true;
        }
    }
    
    void Update()
    {
        if (!playerDead)
        {
            // phase control
            if (transform.eulerAngles.z <= phaseOneLimit && transform.eulerAngles.z >= 0)
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

            if (transform.eulerAngles.z > phaseFourLimit)
            {
                //if (!playerDead)
                //{#
                
                phaseFour();
                //}
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
                transform.Rotate(new Vector3(0,0,1), Time.deltaTime * GM.difficultySpeed);
            }

            if (currentPhase == 4)
            {
                transform.Rotate(new Vector3(0,0,1), Time.deltaTime * GM.eatPlayerSpeed);
                playerDead = true;
                GM.playerDead = true;

                // wait till rotation = player position
                if (transform.eulerAngles.z > 110f && transform.eulerAngles.z < 250f)
                {
                    playerDeadAnimations();
                    GM.gameOver(1f);
                }

                if (transform.eulerAngles.z >= 190f && !splashHitPlayed)
                {
                    splashHitPlayed = true;
                    if (GM.leftActive)
                    {
                        GM.waterSplash("big", new Vector3(spawn.right1.transform.position.x + 1f, spawn.right1.transform.position.y, spawn.right1.transform.position.z));
                    }
                    else
                    {
                        GM.waterSplash("big", spawn.left1.transform.position);
                    }

                    audio.sound(audio.splashIn, 1, 1);
                    
                }

                // when in water, game over
                if (transform.eulerAngles.z >= 250)
                {
                    rotate = false;
                    killPlayer();
                    //GM.gameOver();
                }
            }

        }
    }

    IEnumerator destroyAfter(GameObject obj, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(obj);
        yield return null;
    }
}