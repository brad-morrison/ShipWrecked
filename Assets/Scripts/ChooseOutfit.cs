using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseOutfit : MonoBehaviour
{
    OutfitStats stats;
    public int outfit;

    public GameObject nameText, reqAmountText, reqTypeText, toUnlockText, bar, arrowRight, arrowLeft, buttonText;
    public Sprite unlocked_bar, unlocked_arrow, locked_bar, locked_arrow;

    public GameObject[] outfits;
    public string[] name;
    public string[] reqAmount;
    public string[] reqType;
    public int currentOutfit;

    bool leftLocked, rightLocked;

    int amountInt;

    // Start is called before the first frame update
    void Start()
    {
        stats = GetComponent<OutfitStats>();
        currentOutfit = PlayerPrefs.GetInt("outfit", 0);

        lockCheckRight();
        lockCheckLeft();
        cycle(outfits[currentOutfit]);


    }

    public void nextOutfit()
    {
        currentOutfit++;
        
        lockCheckLeft();
        lockCheckRight();
        cycle(outfits[currentOutfit]);
    }

    public void prevOutfit()
    {
        
        currentOutfit--;
        
        
        lockCheckLeft();
        lockCheckRight();
        cycle(outfits[currentOutfit]);
    }

    void lockCheckRight()
    {
        if (currentOutfit == outfits.Length - 1)
        {
            print("right locked");
            rightLocked = true;
            arrowRight.GetComponent<BoxCollider2D>().enabled = false;
            arrowRight.GetComponent<SpriteRenderer>().sprite = locked_arrow;
        }
        
        if (currentOutfit != outfits.Length - 1)
        {
            print("right unlocked");
            rightLocked = false;
            arrowRight.GetComponent<BoxCollider2D>().enabled = true;
            arrowRight.GetComponent<SpriteRenderer>().sprite = unlocked_arrow;
        }

        
    }

    void lockCheckLeft()
    {
        if (currentOutfit == 0)
        {
            print("left locked");
            leftLocked = true;
            arrowLeft.GetComponent<BoxCollider2D>().enabled = false;
            arrowLeft.GetComponent<SpriteRenderer>().sprite = locked_arrow;   
        }
        
        if (currentOutfit != 0)
        {
            print("left unlocked");
            leftLocked = false;
            arrowLeft.GetComponent<BoxCollider2D>().enabled = true;
            arrowLeft.GetComponent<SpriteRenderer>().sprite = unlocked_arrow;
        }

    }

    void cycle(GameObject activeObj)
    {
        // sprite
        foreach(GameObject outfit in outfits)
        {
            outfit.GetComponent<SpriteRenderer>().enabled = false;
        }

        activeObj.GetComponent<SpriteRenderer>().enabled = true;

        // name
        string nameParse = name[currentOutfit].Replace("\\n", "\n"); 
        nameText.GetComponent<TextMesh>().text = nameParse;
        nameText.transform.GetChild(0).GetComponent<TextMesh>().text = nameParse;

        // reqAmount
        reqAmountText.GetComponent<TextMesh>().text = reqAmount[currentOutfit];
        reqAmountText.transform.GetChild(0).GetComponent<TextMesh>().text = reqAmount[currentOutfit];

        // reqType
        string reqParse = reqType[currentOutfit].Replace("\\n", "\n");
        reqTypeText.GetComponent<TextMesh>().text = reqParse;

        checkReq();
    }

    void checkReq()
    {
        amountInt = int.Parse(reqAmount[currentOutfit]);

        if (reqType[currentOutfit].Contains("total") && stats.totalKillCount >= amountInt)
        {
            unlockOutfit();    
        }
        else if (reqType[currentOutfit].Contains("shark") && stats.sharkCount >= amountInt)
        {
            unlockOutfit(); 
        }
        else if (reqType[currentOutfit].Contains("octo") && stats.octoCount >= amountInt)
        {
            unlockOutfit(); 
        }
        else if(reqType[currentOutfit].Contains("puff") && stats.puffCount >= amountInt)
        {
            unlockOutfit(); 
        }
        else
        {
            lockOutfit();
        }
    }



    void unlockOutfit()
    {
        reqAmountText.GetComponent<TextMesh>().text = "unlocked";
        reqAmountText.transform.GetChild(0).GetComponent<TextMesh>().text = "unlocked";
        reqTypeText.GetComponent<TextMesh>().text = "";
        toUnlockText.GetComponent<TextMesh>().text = "";

        // make sprite white

        // make select button unlocked
        bar.GetComponent<SpriteRenderer>().sprite = unlocked_bar;
        bar.GetComponent<BoxCollider2D>().enabled = true;
        buttonText.active = true;
    }

    void lockOutfit()
    {
        // reqAmount
        reqAmountText.GetComponent<TextMesh>().text = reqAmount[currentOutfit];
        reqAmountText.transform.GetChild(0).GetComponent<TextMesh>().text = reqAmount[currentOutfit];

        // reqType
        string reqParse = reqType[currentOutfit].Replace("\\n", "\n");
        reqTypeText.GetComponent<TextMesh>().text = reqParse;

        toUnlockText.GetComponent<TextMesh>().text = "to unlock";

        // make sprite black
        outfits[currentOutfit].GetComponent<SpriteRenderer>().color = Color.black;

        // make select button locked
        bar.GetComponent<SpriteRenderer>().sprite = locked_bar;
        bar.GetComponent<BoxCollider2D>().enabled = false;
        buttonText.active = false;
    }

    public void choose()
    {
        outfit = currentOutfit;
        PlayerPrefs.SetInt("outfit", outfit);
    }

    void Update()
    {
        if (leftLocked)
        {
            arrowLeft.GetComponent<SpriteRenderer>().sprite = locked_arrow;
        }

        if (rightLocked)
        {
            arrowRight.GetComponent<SpriteRenderer>().sprite = locked_arrow;
        }
    }
}
