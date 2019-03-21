using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outfits : MonoBehaviour
{
    GameManager GM;
    
    public GameObject pirate, pirateShoot, pirateKnife;
    public GameObject skeleton, skeletonShoot, skeletonKnife;
    public GameObject tinman, tinmanShoot, tinmanKnife;
    public GameObject zombie, zombieShoot, zombieKnife;
    public GameObject wizard, wizardKnife, wizardShoot;

    int outfit;
    
    // Start is called before the first frame update
    void Awake()
    {
        GM = GameObject.Find("scripts").GetComponent<GameManager>();
        outfit = PlayerPrefs.GetInt("outfit", 0); //replace this with playerPrefs
        activateOutfit();
    }

    void activateOutfit()
    {
        switch(outfit)
        {
            case 0:
                pirate.SetActive(true);
                GM.player = pirate;
                GM.knifePrefab = pirateKnife;
                GM.shootPrefab = pirateShoot;
                break;

            case 1:
                skeleton.SetActive(true);
                GM.player = skeleton;
                GM.knifePrefab = skeletonKnife;
                GM.shootPrefab = skeletonShoot;
                break;

            case 2:
                tinman.SetActive(true);
                GM.player = tinman;
                GM.knifePrefab = tinmanKnife;
                GM.shootPrefab = tinmanShoot;
                break;

            case 3:
                zombie.SetActive(true);
                GM.player = zombie;
                GM.knifePrefab = zombieKnife;
                GM.shootPrefab = zombieShoot;
                break;

            case 4:
                wizard.SetActive(true);
                GM.player = wizard;
                GM.knifePrefab = wizardKnife;
                GM.shootPrefab = wizardShoot;
                break;
        }
    }
}
