using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    PlayerAnimations PlayerAnimations_s;
    Spawner Spawner_s;
    Audio audio;
    public Shark Shark_s;
    public Octopus Octopus_s;
    public PufferFish PufferFish_s;
    
    public GameObject audioPrefab;
    
    public GameObject camera;
    // outfit stuff
    public GameObject player;
    public string outfit;
    public GameObject knifePrefab, shootPrefab;
    //

    // PLayerpref stuff
    public int sharkCount, octoCount, puffCount, totalKillCount, highScore;
    //
    public GameObject currentEnemy;
    public GameObject blood, ink, waterSplashPrefab, waterSplashSmallPrefab;

    public float waterBreakSpeed, difficultySpeed, eatPlayerSpeed; // enemy speed controls

    public bool leftActive, rightActive; // used to check which way the enemy is attacking from
    public string enemyType; // used to check which type of enemy is attacking 

    // score
    public GameObject scoreTextFront, scoreTextBack, highscoreFloatingText, highscoreFloatingText2;
    public int score;

    // UI
    public GameObject UI, scoreTextGO, scoreTextGOBack, bestText, bestTextBack, treasureChest;
    public Sprite chestOpen;


    public bool playerDead, hitWaterSplash, soundPlayed;

    void Start()
    {
        PlayerAnimations_s = player.GetComponent<PlayerAnimations>();
        Spawner_s = GameObject.Find("scripts").GetComponent<Spawner>();
        audio = GameObject.Find("AUDIO").GetComponent<Audio>();

        // get player prefs
        sharkCount = PlayerPrefs.GetInt("s", 0);
        octoCount = PlayerPrefs.GetInt("o", 0);
        puffCount = PlayerPrefs.GetInt("p", 0);
        highScore = PlayerPrefs.GetInt("h", 0);
    
        Spawner_s.init();
    }

    public void inputCheck(string direction)
    {
        if (!playerDead)
        {
            if (direction == "left" && leftActive)
            {
                playerCorrect();
            }
            
            if (direction == "right" && rightActive)
            {
                //Debug.Log("moving " + direction + " while playerDead is " + playerDead + " enemy rotation z is: " + currentEnemy.transform.eulerAngles.z);
                playerCorrect();
            }
            
            if (direction == "left" && rightActive)
            {
                playerWrong();
            }

            if (direction == "right" && leftActive)
            {
                playerWrong();
            }
        }
    }

    void playerCorrect()
    {
        if (leftActive)
        {
            stopRotations();
            PlayerAnimations_s.moveLeft();
        }
        
        if (rightActive)
        {
            stopRotations();
            PlayerAnimations_s.moveRight();
        }
    }

    void playerWrong()
    {
        if (enemyType == "shark")
        {
            currentEnemy.GetComponent<Shark>().killPlayer();
        }

        if (enemyType == "octopus")
        {
            currentEnemy.GetComponent<Octopus>().killPlayer();
        }

        if (enemyType == "pufferfish")
        {
            currentEnemy.GetComponent<PufferFish>().killPlayer();
        }
    }

    void stopRotations()
    {
        if (enemyType == "shark" && currentEnemy.transform.eulerAngles.z > 85)
        {
            currentEnemy.GetComponent<Shark>().rotate = false;
        }

        if (enemyType == "octopus" && currentEnemy.transform.eulerAngles.z > 85)
        {
            currentEnemy.GetComponent<Octopus>().rotate = false;
        }

        if (enemyType == "pufferfish" && currentEnemy.transform.eulerAngles.z > 85)
        {
            currentEnemy.GetComponent<PufferFish>().rotate = false;
        }
    }

    void sharkInteraction()
    {
        Shark_s.sharkDeath();
        audio.sound(audio.sword, 0.4f, "random");
        audio.sound(audio.slice, 0.8f, "random");
        addToScore();
        nextEnemy();
    }

    void octopusInteraction()
    {
        Octopus_s.octopusInteraction();
    }

    public void octopusInteractionCheck(string direction)
    {
        if (direction == "right" && leftActive)
        {
            Octopus_s.octopusDeath();
            audio.sound(audio.sword, 0.4f, 1.3f);
            audio.sound(audio.slice, 0.8f, "random");
            addToScore();
            nextEnemy();
        }
        else if (direction == "left" && rightActive)
        {
            Octopus_s.octopusDeath();
            audio.sound(audio.sword, 0.4f, 1.3f);
            audio.sound(audio.slice, 0.8f, "random");
            addToScore();
            nextEnemy();
        }
        else
        {
            if (Octopus_s.readyForInput)
            {
                Octopus_s.killPlayer2();
            }
        }
    }

    public void pufferfishInteractionCheck(string direction)
    {
        if (direction == "right" && rightActive)
        {
            audio.sound(audio.gun, 1f, 1.1f);
            PufferFish_s.pufferFishDeath();
            addToScore();
            //nextEnemy();
        }
        else if (direction == "left" && leftActive)
        {
            audio.sound(audio.gun, 1f, 1.1f);
            PufferFish_s.pufferFishDeath();
            addToScore();
            //nextEnemy();
        }
        else
        {
            PufferFish_s.killPlayer2();
        }
    }
    
    void pufferfishInteraction()
    {
        audio.sound(audio.swordMiss, 1f, 1.1f);
        audio.sound(audio.blowUp, 0.8f, 1f);
        PufferFish_s.pufferfishInteraction();
        
    }

    public void playerHit()
    {
        if (enemyType == "shark")
        {
            sharkInteraction();
        }

        if (enemyType == "octopus")
        {
            octopusInteraction();
        }

        if (enemyType == "pufferfish")
        {
            pufferfishInteraction();
        }
    }

    public void nextEnemy()
    {
        // update fins
        camera.GetComponent<CamShake>().shake(0.2f); // temp camshake location
        Spawner_s.createNewFin("random");
        Spawner_s.moveFins();
        Spawner_s.spawnEnemy();
    }

    public void bloodSpurt(Transform position)
    {
        GameObject bloodInstance = Instantiate(blood, position.position, Quaternion.identity);

        // flip blood sprite if active
        if (rightActive)
        {
            bloodInstance.transform.eulerAngles = new Vector3(0,180,0);
        }
    }
    
    public void bloodSpurt(Transform position, int amount)
    {
        if (amount == 2)
        {
            GameObject bloodInstance = Instantiate(blood, position.position, Quaternion.identity);
            GameObject bloodInstance2 = Instantiate(blood, position.position, Quaternion.identity);
            bloodInstance2.transform.eulerAngles = new Vector3(0,180,0);

            // flip blood sprite if active
            if (rightActive)
            {
                bloodInstance.transform.eulerAngles = new Vector3(0,180,0);
                bloodInstance2.transform.eulerAngles = new Vector3(0,0,0);
            }
        }
    }

    public void bloodSpurtInk(Transform position, int amount)
    {
        if (amount == 2)
        {
            GameObject bloodInstance = Instantiate(ink, position.position, Quaternion.identity);
            GameObject bloodInstance2 = Instantiate(ink, position.position, Quaternion.identity);
            bloodInstance2.transform.eulerAngles = new Vector3(0,180,0);

            // flip blood sprite if active
            if (rightActive)
            {
                bloodInstance.transform.eulerAngles = new Vector3(0,180,0);
                bloodInstance2.transform.eulerAngles = new Vector3(0,0,0);
            }
        }
    }
    
    void addToScore()
    {
        score = score + 1;
        scoreTextFront.GetComponent<TextMesh>().text = score.ToString();
        scoreTextBack.GetComponent<TextMesh>().text = score.ToString();
        // temp difficulty control
        difficultySpeed = difficultySpeed + 1f;

        if (score == highScore + 1)
        {
            StartCoroutine(activeFor(0.5f, highscoreFloatingText));
        }
    }

    IEnumerator activeFor(float secs, GameObject obj)
    {
        obj.active = true;
        audio.sound(audio.highScore, 1, 1);
        yield return new WaitForSeconds(secs);
        obj.active = false;
    }

    public void gameOver()
    {
        //Destroy(currentEnemy);
        // open UI here

        // PP
        setPlayerPrefs();

        UI.active = true;
        scoreTextGO.GetComponent<TextMesh>().text = score.ToString();
        scoreTextGOBack.GetComponent<TextMesh>().text = score.ToString();
        bestText.GetComponent<TextMesh>().text = PlayerPrefs.GetInt("h", 0).ToString();
        bestTextBack.GetComponent<TextMesh>().text = PlayerPrefs.GetInt("h", 0).ToString();
        scoreTextFront.active = false;
        scoreTextBack.active = false;

        // check for high score
        if (highScore < score)
        {
            StartCoroutine(openChest());
            StartCoroutine(showHighText2());
        }

        //Time.timeScale = 0;
        //Application.LoadLevel(Application.loadedLevel);
    }
    
    IEnumerator openChest()
    {
        yield return new WaitForSeconds(1);
        treasureChest.GetComponent<SpriteRenderer>().sprite = chestOpen;
    }

    IEnumerator showHighText2()
    {
        yield return new WaitForSeconds(1);
        highscoreFloatingText2.active = true;
        if (!soundPlayed)
        {
            audio.sound(audio.highScore, 1, 1);
        }
        yield return null;
        soundPlayed = true;
    }

    public void gameOver(float time)
    {
        //Destroy(currentEnemy);
        // open UI here
        
        // PP
        //setPlayerPrefs();

        StartCoroutine(gameOverAfter(time));
    }

    public void waterSplash(string size, Vector3 position)
    {
        if (size == "small")
        {
            GameObject water = Instantiate(waterSplashSmallPrefab, position, Quaternion.identity);
        }
        else
        {
            GameObject water = Instantiate(waterSplashPrefab, position, Quaternion.identity);
        }

        audio.sound(audio.waterButton1, 1, 1);
    }

    void setPlayerPrefs()
    {
        // PP
        totalKillCount = sharkCount + octoCount + puffCount;
        PlayerPrefs.SetInt("s", sharkCount);
        PlayerPrefs.SetInt("o", octoCount);
        PlayerPrefs.SetInt("p", puffCount);
        PlayerPrefs.SetInt("t", totalKillCount);

        if (highScore < score)
        {
            PlayerPrefs.SetInt("h", score);
        }
    }

    IEnumerator gameOverAfter(float time)
    {
        yield return new WaitForSeconds(time);
        gameOver();
        yield return null;
    }


}