using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    PlayerAnimations PlayerAnimations_s;
    Spawner Spawner_s;
    Enemy Enemy_s;
    AudioSource audio;
    public GameObject audioPrefab;
    
    public GameObject camera;
    public GameObject player;
    public GameObject currentEnemy;
    public GameObject blood;

    public float waterBreakSpeed, difficultySpeed, eatPlayerSpeed; // enemy speed controls

    public bool leftActive, rightActive; // used to check which way the enemy is attacking from
    public string enemyType; // used to check which type of enemy is attacking 

    // score
    public GameObject scoreTextFront, scoreTextBack;
    public int score;

    void Start()
    {
        PlayerAnimations_s = player.GetComponent<PlayerAnimations>();
        Spawner_s = GameObject.Find("scripts").GetComponent<Spawner>();
        audio = GetComponent<AudioSource>();
    }

    public void inputCheck(string direction)
    {
        if (direction == "left" && leftActive)
        {
            playerCorrect();
        }
        else if (direction == "right" && rightActive)
        {
            playerCorrect();
        }
        else
        {
            playerWrong();
        }
    }

    void playerCorrect()
    {
        if (leftActive)
        {
            PlayerAnimations_s.moveLeft();
        }
        else
        {
            PlayerAnimations_s.moveRight();
        }
    }

    void playerWrong()
    {
        Enemy_s.killPlayer();
    }

    void sharkInteraction()
    {
        Enemy_s.sharkDeath();
        addToScore();
        nextEnemy();
    }

    void octopusInteraction()
    {
        Enemy_s.octopusDeath();
        addToScore();
        nextEnemy();
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
    }

    public void nextEnemy()
    {
        // update fins
        Spawner_s.createNewFin("random");
        Spawner_s.moveFins();
        Spawner_s.spawnEnemy();
        // update current enemy script
        Enemy_s = currentEnemy.GetComponent<Enemy>();
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

    void addToScore()
    {
        score = score + 1;
        scoreTextFront.GetComponent<TextMesh>().text = score.ToString();
        scoreTextBack.GetComponent<TextMesh>().text = score.ToString();
    }

    public void sound(AudioClip clip, float volume, float pitch)
    {
        StartCoroutine(playSound(clip, volume, pitch));
    }
    
    public void gameOver()
    {
        Destroy(currentEnemy);
        // open UI here
    }

    //
    // coroutines
    //

    IEnumerator playSound(AudioClip clip, float volume, float pitch)
    {
        GameObject _audio = Instantiate(audioPrefab);
        audio = _audio.GetComponent<AudioSource>();
    	audio.volume = volume;
        audio.pitch = pitch;
        audio.PlayOneShot(clip);
        if(!audio.isPlaying)
        {
            Destroy(_audio);
        }
    	yield return null;
    }
}