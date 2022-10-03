using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    GameManager GM;
    Audio audio;

    // gameobjects of fins and targets
    public GameObject left3, left2, left1, right3, right2, right1; // GameObjects of fin targets
    public GameObject fin1, fin2, fin3, newFin; // GameObjects of fins
    public GameObject finShark, finOctopus, finPufferfish; // Prefabs of fin objects
    public GameObject shark, octopus, pufferfish; // Prefabs of enemy objects
    
    // descriptive strings
    public string pos1, pos2, pos3, pos4; // describes the direction of each fin (left or right)
    public string pos1Type, pos2Type, pos3Type, pos4Type; // describes the enemy type of each fin
    
    public float finSlideSpeed;
    
    bool RandPos => ( Random.Range(0, 2) == 1 );

    void Start()
    {
        GM = GameObject.Find("scripts").GetComponent<GameManager>();
        audio = GameObject.Find("AUDIO").GetComponent<Audio>();
    }
    
    public void init()
    {
        string RandPos3 = RandPos ? "left" : "right";
        string RandPos2 = RandPos ? "left" : "right";
        string RandPos1 = RandPos ? "left" : "right";

        //pos 3
        if (RandPos3 == "left")
        {   
            // create fin object
            fin3 = Instantiate(finShark, left3.transform.position, Quaternion.identity);

            // set descriptive strings
            pos3 = "left";
            pos3Type = "shark";
        }
        else
        {   // create fin object and flip
            fin3 = Instantiate(finShark, right3.transform.position, Quaternion.identity);
            fin3.transform.eulerAngles = new Vector3(0f,180f,0f);

            // set descriptive strings
            pos3 = "right";
            pos3Type = "shark";
        }

        //pos 2
        if (RandPos2 == "left")
        {   
            fin2 = Instantiate(finShark, left2.transform.position, Quaternion.identity);  

            // set descriptive strings
            pos2 = "left";
            pos2Type = "shark";
        }
        else
        {  
            fin2 = Instantiate(finShark, right2.transform.position, Quaternion.identity);
            fin2.transform.eulerAngles = new Vector3(0f,180f,0f);    

            // set descriptive strings
            pos2 = "right";
            pos2Type = "shark";
        }

        fin3.name = "fin3";
        fin2.name = "fin2";
        
        createNewFin("shark");
        moveFins();
        spawnEnemy();

    }
    
    public void createNewFin(string enemy)
    {
        string enemyType = null;
        GameObject target;

        // choose direction of spawn and set temp object
        string direction  = RandPos ? "left" : "right";
        if (direction == "left")
        {
            target = left3;
            pos4 = "left"; //
        }
        else
        {
            target = right3;
            pos4 = "right"; //
        }

        // choose type of enemy to spawn (unless passed in by parameter)
        if (enemy == "random")
        {
            int dice = Random.Range(0,11);
            if (dice < 6)
            {
                enemyType = "shark";
                pos4Type = "shark";
            }
            
            if (dice >= 6 && dice < 8)
            {
                enemyType = "octopus";
                pos4Type = "octopus";
            }

            if (dice >= 8)
            {
                //enemyType = "shark";
                //pos4Type = "shark";
                enemyType = "pufferfish";
                pos4Type = "pufferfish";
            }
        }
        else
        {
            enemyType = enemy;
            pos4Type = enemy;
        }

        // create new fin
        if (enemyType == "shark")
        {
            newFin = Instantiate(finShark, target.transform.position, Quaternion.identity);
        }

         if (enemyType == "octopus")
         {
             newFin = Instantiate(finOctopus, target.transform.position, Quaternion.identity);
         }

         if (enemyType == "pufferfish")
         {
             newFin = Instantiate(finPufferfish, target.transform.position, Quaternion.identity);
         }

         // flip if on right
         if (direction == "right")
         {
             newFin.transform.eulerAngles = new Vector3(0f,180f,0f);
         }


    }

    public void moveFins()
    {
        // move fins
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

        Destroy(fin1); //
        fin1 = null; //

        fin2.name = "fin1";
        fin1 = GameObject.Find("fin1");
        pos1Type = pos2Type;

        fin3.name = "fin2";
        fin2 = GameObject.Find("fin2");
        pos2Type = pos3Type;

        newFin.name = "fin3";
        fin3 = GameObject.Find("fin3");
        pos3Type = pos4Type;
        
    }

    public void spawnEnemy()
    {
        GameObject currentEnemy = null;



        // which direction is next enemy?
        if (pos1 == "left")
        {
            GM.leftActive = true;
            GM.rightActive = false;

            // is next enemy a shark?
            if (pos1Type == "shark")
            {
                currentEnemy = Instantiate(shark, shark.transform.position, Quaternion.identity);
                currentEnemy.transform.eulerAngles = new Vector3(0,180,0);
            }

            // is next enemy an octopus?
            if (pos1Type == "octopus")
            {
                currentEnemy = Instantiate(octopus, octopus.transform.position, Quaternion.identity);
                currentEnemy.transform.eulerAngles = new Vector3(0,180,0);
            }

            // is next enemy a fish?
            if (pos1Type == "pufferfish")
            {
                currentEnemy = Instantiate(pufferfish, pufferfish.transform.position, Quaternion.identity);
                currentEnemy.transform.eulerAngles = new Vector3(0,180,0);
            }

            GM.waterSplash("small", left1.transform.position);
            audio.sound(audio.waterButton2, 2, 1);
            
        }

        if (pos1 == "right")
        {
            GM.rightActive = true;
            GM.leftActive = false;

            // is next enemy a shark?
            if (pos1Type == "shark")
            {
                currentEnemy = Instantiate(shark, shark.transform.position, Quaternion.identity);
                currentEnemy.transform.eulerAngles = new Vector3(0,0,0);
            }

            // is next enemy an octopus?
            if (pos1Type == "octopus")
            {
                currentEnemy = Instantiate(octopus, octopus.transform.position, Quaternion.identity);
                currentEnemy.transform.eulerAngles = new Vector3(0,0,0);
            }

            // is next enemy a fish?
            if (pos1Type == "pufferfish")
            {
                currentEnemy = Instantiate(pufferfish, pufferfish.transform.position, Quaternion.identity);
                currentEnemy.transform.eulerAngles = new Vector3(0,0,0);
            }

            GM.waterSplash("small", new Vector3(right1.transform.position.x + 1f, right1.transform.position.y, right1.transform.position.z));
        }

        GM.currentEnemy = currentEnemy;
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
