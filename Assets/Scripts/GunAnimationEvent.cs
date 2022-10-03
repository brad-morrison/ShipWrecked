using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAnimationEvent : MonoBehaviour
{
    PufferFish pf;
    GameManager GM;

    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.Find("scripts").GetComponent<GameManager>();
        pf = GM.currentEnemy.GetComponent<PufferFish>();
    }

    void kill()
    {
        pf.kill();
    }
}
