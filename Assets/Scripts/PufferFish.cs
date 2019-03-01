using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PufferFish : MonoBehaviour
{
    GameManager GM;
    InputActions InputActions_s;
    Audio audio;
    GameObject camera;
    Spawner spawn;

    public string enemyType;
    public bool rotate = true;

    // octopus prefabs and sprites
    public GameObject pufferFishDeathAnimation, spinAnimation;
    public GameObject puffer, puffed, halfPuffed, puffFly, gun_shot, playerPistol;

    public GameObject currentEnemy, player, spin;
    public GameObject puffTemp, pistol;
    public Sprite halfBlown, fullBlown;

    public float moveSpeed;
    bool splashHitPlayed;

    Vector3 playerStartPosition, gunLeft, gunRight;
    public Vector3 leftCorner;

    // phase rotations
    public int currentPhase;
    float phaseOneLimit = 55f;       // fast rotation at beginning
    float phaseTwoLimit = 80f;       // main difficulty rotation
    float phaseThreeLimit = 88f;       // main difficulty rotation
    float phaseFourLimit = 93f;     // final stage before death

    bool playerDead = false; // used to stop rotation update from taking over the death movement
    bool deadAnimationsPlayed = false;

    // Start is called before the first frame update
    void Start()
    {
        InputActions_s = GameObject.Find("scripts").GetComponent<InputActions>();
        audio = GameObject.Find("AUDIO").GetComponent<Audio>();
        GM = GameObject.Find("scripts").GetComponent<GameManager>();
        GM.enemyType = enemyType;
        GM.PufferFish_s = this;
        spawn = GameObject.Find("scripts").GetComponent<Spawner>();

        player = GameObject.Find("player_parent");
        playerStartPosition = player.transform.position;
        leftCorner = new Vector3(-7.88f, 3.32f, 0f);
        gunLeft = new Vector3(-1.62f, 3.07f, -0.176f);
        gunRight = new Vector3(2.33f, 3.07f, -0.176f);
    }

    public void stopRotation()
    {
        rotate = false;
    }

    public void killPlayer()
    {
        playerDead = true;
        GM.playerDead = true;
        currentPhase = 4;
    }

    // kill player from flying
    public void killPlayer2()
    {
        GameObject fly;

        //GM.gameOver();
        
        if (GM.rightActive)
        {
            fly = Instantiate(puffFly, puffFly.transform.position, Quaternion.identity);
        }
        else
        {
            fly = Instantiate(puffFly, leftCorner, Quaternion.identity);
            fly.transform.eulerAngles = new Vector3(0,180,0);
        }
        audio.sound(audio.blowDown, 1f, 1f);

        Destroy(puffTemp);
    }

    // calls when fly animation hits player
    public void flown()
    {
        //GM.gameOver();
        playerDead = true;
        GM.camera.GetComponent<CamShake>().shake(0.2f);
        GM.bloodSpurt(player.transform, 2);
        audio.sound(audio.slice, 1, 1);
        Destroy(GM.player);
        GM.playerDead = true;
        GM.gameOver(0.5f);
    }

    public void pufferFishDeath()
    {
        InputActions_s.pufferfishActive = false;

        GameObject gun;

        player.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        if (GM.leftActive)
        {
            pistol = Instantiate(playerPistol, playerPistol.transform.position, Quaternion.identity);
            pistol.transform.eulerAngles = new Vector3(0,180,0);
            gun = Instantiate(gun_shot, gunLeft, Quaternion.identity);
        }
        else
        {
            pistol = Instantiate(playerPistol, playerPistol.transform.position, Quaternion.identity);
            
            gun = Instantiate(gun_shot, gunRight, Quaternion.identity);
            gun.transform.eulerAngles = new Vector3(0,180,0);
        }
        
    }

    public void kill()
    {
        GM.camera.GetComponent<CamShake>().shake(0.2f);
        GM.bloodSpurt(puffTemp.transform, 2);
        Destroy(puffTemp);
        Destroy(pistol);
        player.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        GM.nextEnemy();
        Destroy(gameObject);
    }

    public void pufferfishInteraction()
    {
        InputActions_s.pufferfishActive = true;
        rotate = false;
        
        //create puffed up fish animation
        puffTemp = Instantiate(puffed, puffer.transform.position, Quaternion.identity);
        puffer.GetComponent<SpriteRenderer>().enabled = false;

        // move puffed up fish
        if (!GM.leftActive)
        {
            StartCoroutine(move(puffTemp, puffFly.transform.position));
        }
        else
        {
            StartCoroutine(move(puffTemp, leftCorner));
            puffTemp.transform.eulerAngles = new Vector3(0,180,0);
        }
    }

    public void pufferfishInteraction2()
    {
        StartCoroutine(killAfter(2f));
    }

    void phaseOne()
    {
        currentPhase = 1;

        // if I need to add phaseOne sprites later
    }

    void phaseTwo()
    {
        currentPhase = 2;
        

    }

    void phaseThree()
    {
        currentPhase = 3;

        puffer.GetComponent<SpriteRenderer>().sprite = halfBlown;
    }

    void phaseFour()
    {
        currentPhase = 4;
        playerDead = true;

        puffer.GetComponent<SpriteRenderer>().sprite = fullBlown;
        //currentEnemy.GetComponent<SpriteRenderer>().sprite = octopusEnd;
    }



    void playerDeadAnimations()
    {

        if (!deadAnimationsPlayed)
        {
            GM.bloodSpurt(GM.player.transform.parent.transform, 2);
            audio.sound(audio.slice, 1, 1);
            GM.player.transform.SetParent(this.transform);
            deadAnimationsPlayed = true;
        }
    }

    // Update is called once per frame
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

            if (transform.eulerAngles.z <= phaseFourLimit && transform.eulerAngles.z >= phaseThreeLimit)
            {
                phaseFour();
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
                playerDead = true;
                transform.Rotate(new Vector3(0,0,1), Time.deltaTime * GM.eatPlayerSpeed);
                playerDead = true;
                GM.playerDead = true;

                // wait till rotation = player position
                if (transform.eulerAngles.z > 112f && transform.eulerAngles.z < 250f)
                {
                    playerDeadAnimations();
                }

                // when in water, game over
                if (transform.eulerAngles.z >= 250)
                {
                    rotate = false;
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
            }

        }
    }

    IEnumerator move(GameObject fin, Vector3 target)
    {
        //audio.soundLong(audio.spin, 0.8f, 1);
        while(fin.transform.position != target)
    	{
    		fin.transform.position = Vector3.MoveTowards(fin.transform.position, target, moveSpeed * Time.deltaTime);
    		yield return new WaitForEndOfFrame();
    	}
        pufferfishInteraction2();
        yield return null;
    }

    IEnumerator killAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if (!playerDead)
        {
            killPlayer2();
        }
        yield return null;
    }

    IEnumerator destroyAfter(GameObject obj, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(obj);
        yield return null;
    }
}
