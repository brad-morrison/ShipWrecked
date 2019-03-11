using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Octopus : MonoBehaviour
{
    GameManager GM;
    InputActions InputActions_s;
    Audio audio;
    GameObject camera;

    public string enemyType;
    public bool rotate = true;

    // octopus prefabs and sprites
    public GameObject octopusDeathAnimation, spinAnimation, octopusFaceHug, faceHug, holdingKnife, octopusWin;
    public Sprite octopusEnd;

    public GameObject currentEnemy, player, spin;

    Vector3 playerStartPosition;

    public bool dead, knifeAnimationOn, readyForInput;

    // phase rotations
    public int currentPhase;
    float phaseOneLimit = 55f;       // fast rotation at beginning
    float phaseTwoLimit = 88f;       // main difficulty rotation
    float phaseThreeLimit = 93f;     // final stage before death
    public float spinSpeed;

    bool playerDead = false; // used to stop rotation update from taking over the death movement
    bool deadAnimationsPlayed;

    // Start is called before the first frame update
    void Start()
    {
        InputActions_s = GameObject.Find("scripts").GetComponent<InputActions>();
        audio = GameObject.Find("AUDIO").GetComponent<Audio>();
        GM = GameObject.Find("scripts").GetComponent<GameManager>();
        GM.enemyType = enemyType;
        GM.Octopus_s = this;

        player = GameObject.Find("player_parent");
        playerStartPosition = player.transform.position;
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

    // kill player from facehug
    public void killPlayer2()
    {
        Destroy(faceHug);
        GameObject octopusWinTemp = Instantiate(octopusWin, octopusWin.transform.position, Quaternion.identity);
        if (GM.leftActive)
        {
            octopusWinTemp.transform.eulerAngles = new Vector3(0,180,0);
        }
        StartCoroutine(destroyAfter(octopusWinTemp, 0.5f));
        Destroy(spin);
        playerDeadAnimations();
        Destroy(GM.player);
        
        //GM.gameOver();
    }

    public void octopusDeath()
    {
        InputActions_s.octopusActive = false;
        dead = true;
        GM.octoCount = GM.octoCount + 1;
        Destroy(spin);
        StopAllCoroutines();
        

        GM.player.GetComponent<SpriteRenderer>().enabled = false;
        knifeAnimationOn = true;
        GameObject knife = Instantiate(GM.knifePrefab, GM.knifePrefab.transform.position, Quaternion.identity);
        if (!GM.leftActive)
        {
            knife.GetComponent<SpriteRenderer>().flipX = true;
        }
        StartCoroutine(restorePlayer(knife, 0.4f));

        GameObject octoTemp;

        // death animation
        if (GM.leftActive)
        {
            octoTemp = Instantiate(octopusDeathAnimation, GM.player.transform.position, Quaternion.identity);
        }
        else
        {
            octoTemp = Instantiate(octopusDeathAnimation, GM.player.transform.position, Quaternion.identity);
            octoTemp.transform.eulerAngles = new Vector3(0, 180, 0);
        }

        Destroy(faceHug);
        GM.bloodSpurtInk(player.transform, 2);
        
        StartCoroutine(destroyAfter(octoTemp, 1f));
        StartCoroutine(destroyAfter(gameObject, 1f));
    }

    public void octopusInteraction()
    {
        InputActions_s.octopusActive = true;
        readyForInput = false;
        //hide current octopus, player and stop rotating
        currentEnemy.GetComponent<SpriteRenderer>().enabled = false;
        //GameObject.Find("player").GetComponent<SpriteRenderer>().enabled = false;
        GM.player.GetComponent<SpriteRenderer>().enabled = false;
        rotate = false;

        //create spinning sprite at enemy location and start coroutine to move
        spin = Instantiate(spinAnimation, currentEnemy.transform.position, Quaternion.identity);
        StartCoroutine(move(spin, playerStartPosition));
    }
    public void octopusInteraction2()
    {
        GM.player.GetComponent<SpriteRenderer>().enabled = true;
        Destroy(spin);
        if (!dead)
        {
            faceHug = Instantiate(octopusFaceHug, octopusFaceHug.transform.position, Quaternion.identity);
            readyForInput = true;
        }

        if (GM.leftActive)
        {
            player.transform.eulerAngles = new Vector3(0,180, 0);
            faceHug.transform.eulerAngles = new Vector3(0,180, 0);
        }
        else
        {
            player.transform.eulerAngles = new Vector3(0,0, 0);
            faceHug.transform.eulerAngles = new Vector3(0,0, 0);
        }

        
        StartCoroutine(killAfter(2f));
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
            GM.gameOver(0.5f);
        }
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

        currentEnemy.GetComponent<SpriteRenderer>().sprite = octopusEnd;
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
                    GM.gameOver();
                }
            }

        }

        if (knifeAnimationOn)
        {
            GM.player.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    IEnumerator destroyAfter(GameObject obj, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(obj);
        yield return null;
    }

    IEnumerator move(GameObject fin, Vector3 target)
    {
        audio.soundLong(audio.spin, 0.8f, 1);
        while(fin.transform.position != target)
    	{
    		fin.transform.position = Vector3.MoveTowards(fin.transform.position, target, spinSpeed * Time.deltaTime);
    		yield return new WaitForEndOfFrame();
    	}
        octopusInteraction2();
        yield return null;
    }

    IEnumerator killAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if(!playerDead)
        {
            killPlayer2();
        }
        yield return null;
    }

    IEnumerator restorePlayer(GameObject knife, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        knifeAnimationOn = false;
        GM.player.GetComponent<SpriteRenderer>().enabled = true;
        Destroy(knife);
        yield return null;
    }
}
