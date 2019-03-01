using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flyChild : MonoBehaviour
{
    public GameObject psGO;
    PufferFish ps;
    GameManager GM;
    // Start is called before the first frame update
    void Start()
    {   
         GM = GameObject.Find("scripts").GetComponent<GameManager>();
         ps = GM.currentEnemy.GetComponent<PufferFish>();
       
    }

    public void childHit()
    {
        ps.flown();
    }
}
