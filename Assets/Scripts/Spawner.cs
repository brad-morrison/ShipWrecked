using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    GameManager GM;

    // gameobjects of fins and targets
    public GameObject left3, left2, left1, right3, right2, right1; // GameObjects of fin targets
    public GameObject fin1, fin2, fin3, newFin; // GameObjects of fins
    public GameObject finShark, finOctopus; // Prefabs of fin objects
    public GameObject shark, octopus; // Prefabs of enemy objects
    
    // descriptive strings
    string pos1, pos2, pos3, pos4; // describes the direction of each fin (left or right)
    string pos1Type, pos2Type, pos3Type, pos4Type; // describes the enemy type of each fin
    
    public float finSlideSpeed;
    
    bool RandPos => ( Random.Range(0, 2) == 1 );

    void Start()
    {
        GM = GameObject.Find("scripts").GetComponent<GameManager>();
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
        }
        else
        {
            target = right3;
        }

        // choose type of enemy to spawn (unless passed in by parameter)
        if (enemy == "random")
        {
            int dice = Random.Range(0,3);
            if (dice != 1)
            {
                enemyType = "shark";
            }
            else
            {
                enemyType = "octopus";
            }
        }
        else
        {
            enemyType = enemy;
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

        // change names and update descriptive strings
        Destroy(fin1);
        fin1.name = "null";
        
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
            // is next enemy a shark?
            if (pos1Type == "shark")
            {
                currentEnemy = Instantiate(shark, left1.transform.position, Quaternion.identity);
            }

            // is next enemy an octopus?
            if (pos1Type == "octopus")
            {
                currentEnemy = Instantiate(octopus, left1.transform.position, Quaternion.identity);
            }
        }
        else
        {
            // is next enemy a shark?
            if (pos1Type == "shark")
            {
                currentEnemy = Instantiate(shark, right1.transform.position, Quaternion.identity);
            }

            // is next enemy an octopus?
            if (pos1Type == "octopus")
            {
                currentEnemy = Instantiate(octopus, right1.transform.position, Quaternion.identity);
            }
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
