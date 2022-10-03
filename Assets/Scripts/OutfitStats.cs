using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutfitStats : MonoBehaviour
{

    public int sharkCount, octoCount, puffCount, totalKillCount, highScore;

    public GameObject shark, octo, puff, total, high;


    // Start is called before the first frame update
    void Awake()
    {
        // get player prefs
        sharkCount = PlayerPrefs.GetInt("s", 0);
        octoCount = PlayerPrefs.GetInt("o", 0);
        puffCount = PlayerPrefs.GetInt("p", 0);
        highScore = PlayerPrefs.GetInt("h", 0);
        totalKillCount = sharkCount + octoCount + puffCount;
        setCounts();
    }

    void setCounts()
    {
        shark.GetComponent<TextMesh>().text = sharkCount.ToString();
        octo.GetComponent<TextMesh>().text = octoCount.ToString();
        puff.GetComponent<TextMesh>().text = puffCount.ToString();
        total.GetComponent<TextMesh>().text = totalKillCount.ToString();
        high.GetComponent<TextMesh>().text = highScore.ToString();
    }

    
}
