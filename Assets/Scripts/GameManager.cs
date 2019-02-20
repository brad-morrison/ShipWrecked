﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    SharkState SharkState_;
    AnimationEvents animations;
    
    public GameObject sharkDiscLeft;
    public GameObject sharkDiscRight;
    public GameObject sharkLeft;
    public GameObject sharkRight;
    public GameObject sharkDeathPrefab;
    public GameObject finPrefab;
    
    public float waterBreakSpeed;
    public float difficultySpeed;
    public float eatPlayerSpeed;
    public float finSlideSpeed;

    public GameObject left3, left2, left1, right3, right2, right1;
    GameObject fin1, fin2, fin3, newFin;
    public GameObject scoreFront, scoreBack;

    public bool leftActive, rightActive, gameOver;

    public string pos1, pos2, pos3, pos4;

    public int score;

    public bool RandPos => ( Random.Range(0, 2) == 1 );

    // Start is called before the first frame update
    void Start()
    {
        animations = GameObject.Find("player").GetComponent<AnimationEvents>(); 

        initStart();
    }

    public void initStart()
    {
        pos1 = RandPos ? "left" : "right";
        pos2 = RandPos ? "left" : "right";
        pos3 = RandPos ? "left" : "right";

        if (pos1 == "left")
        {
            //shark is left
            fin1 = Instantiate(finPrefab, left1.transform.position, Quaternion.identity);
            fin1.name = "fin1";
        }
        else
        {
            //shark is right
            fin1 = Instantiate(finPrefab, right1.transform.position, Quaternion.identity);
            fin1.GetComponent<SpriteRenderer>().flipX = true;
            fin1.name = "fin1";
        }

        if (pos2 == "left")
        {
            fin2 = Instantiate(finPrefab, left2.transform.position, Quaternion.identity);
            fin2.name = "fin2";
        }
        else
        {
            fin2 = Instantiate(finPrefab, right2.transform.position, Quaternion.identity);
            fin2.GetComponent<SpriteRenderer>().flipX = true;
            fin2.name = "fin2";
        }

        if (pos3 == "left")
        {
            fin3 = Instantiate(finPrefab, left3.transform.position, Quaternion.identity);
            fin3.name = "fin3";
        }
        else
        {
            fin3 = Instantiate(finPrefab, right3.transform.position, Quaternion.identity);
            fin3.GetComponent<SpriteRenderer>().flipX = true;
            fin3.name = "fin3";
        }

        if (pos1 == "left")
        {
            leftActive = true;
            sharkDiscLeft.GetComponent<SharkState>().rotate = true;
        }
        else
        {
            rightActive = true;
            sharkDiscRight.GetComponent<SharkState>().rotate = true;
        }

        newFinCreate();
    }

    public void playerTouched(string direction)
    {
        if (direction == "left" && leftActive)
        {
            animations.left();
            playerCorrect();
        }
        else if (direction == "right" && rightActive)
        {
            animations.right();
            playerCorrect();
        }
        else
        {
            playerWrong();
        }
    }

    public void playerCorrect()
    {
        addToScore();
        
        // spawn shark death animation
        if (leftActive)
        {
            Instantiate(sharkDeathPrefab, sharkLeft.transform.position, sharkLeft.transform.rotation);
        }
        else
        {
            Instantiate(sharkDeathPrefab, sharkRight.transform.position, sharkRight.transform.rotation);
        }
        
        // reset discs
        sharkDiscLeft.transform.eulerAngles = new Vector3(0,180,0);
        sharkDiscRight.transform.eulerAngles = new Vector3(0,0,0);

        //move fins inward
        moveFins();
        makeSharkAppear();

    }

    public void playerWrong()
    {
        if (leftActive)
        {
            sharkDiscLeft.GetComponent<SharkState>().lose = true;
        }
        else
        {
            sharkDiscRight.GetComponent<SharkState>().lose = true;
        }
    }

    public void newFinCreate()
    {
        string newFinPos = RandPos ? "left" : "right";
        
        if (newFinPos == "left")
        {
            newFin = Instantiate(finPrefab, left3.transform.position, Quaternion.identity);
            newFin.name = "newFin";
            pos4 = "left";
        }
        else
        {
            newFin = Instantiate(finPrefab, right3.transform.position, Quaternion.identity);
            newFin.GetComponent<SpriteRenderer>().flipX = true;
            newFin.name = "newFin";
            pos4 = "right";
        }
    }
    
    public void moveFins()
    {
        if (pos2 == "left")
        {
            StartCoroutine(moveFin(fin2, left1));
            pos1 = "left";
        }
        else
        {
            StartCoroutine(moveFin(fin2, right1));
            pos1 = "right";
        }

        if (pos3 == "left")
        {
            StartCoroutine(moveFin(fin3, left2));
            pos2 = "left";
        }
        else
        {
            StartCoroutine(moveFin(fin3, right2));
            pos2 = "right";
        }

        if (pos4 == "left")
        {
            StartCoroutine(moveFin(newFin, left3));
            pos3 = "left";
        }
        else
        {
            StartCoroutine(moveFin(newFin, right3));
            pos3 = "right";
        }
        

        Destroy(fin1);
        fin1.name = "null";
        
        fin2.name = "fin1";
        fin1 = GameObject.Find("fin1");

        fin3.name = "fin2";
        fin2 = GameObject.Find("fin2");

        newFin.name = "fin3";
        fin3 = GameObject.Find("fin3");
        
        newFinCreate();
    }

    void makeSharkAppear()
    {
        if (pos1 == "left")
        {
            leftActive = true;
            rightActive = false;
            sharkDiscLeft.GetComponent<SharkState>().rotate = true;
            sharkDiscRight.GetComponent<SharkState>().rotate = false;
        }
        else
        {
            rightActive = true;
            leftActive = false;
            sharkDiscRight.GetComponent<SharkState>().rotate = true;
            sharkDiscLeft.GetComponent<SharkState>().rotate = false;
        }
    }

    void addToScore()
    {
        score = score + 1;
        scoreFront.GetComponent<TextMesh>().text = score.ToString();
        scoreBack.GetComponent<TextMesh>().text = score.ToString();
    }

    public void GameOver()
    {
        gameOver = true;
    }

    IEnumerator moveFin(GameObject fin, GameObject target)
    {
        while(fin.transform.position != target.transform.position)
    	{
    		fin.transform.position = Vector3.MoveTowards(fin.transform.position, target.transform.position, finSlideSpeed * Time.deltaTime);
    		yield return new WaitForEndOfFrame();
    	}
        yield return null;
    }
}
