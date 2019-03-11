using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMenuMovement : MonoBehaviour
{
    public GameObject targetSettings, targetMain, targetOutfits;
    public float camMoveSpeed;

    string startLocation;
    
    void Awake()
    {
        startLocation = PlayerPrefs.GetString("cam", "main");
        startLocationSet();   
    }

    public void startLocationSet()
    {
        if (startLocation == "outfits")
        {
            transform.position = targetOutfits.transform.position;
            PlayerPrefs.SetString("cam", "main");
        }

        if (startLocation == "settings")
        {
            transform.position = targetSettings.transform.position;
            PlayerPrefs.SetString("cam", "main");
        }
    }
    
    public void move(GameObject target)
    {
        StopAllCoroutines();
        StartCoroutine(moveCam(this.gameObject, target));
    }

    IEnumerator moveCam(GameObject cam, GameObject target)
    {
        while(cam.transform.position != target.transform.position)
    	{
    		cam.transform.position = Vector3.Lerp(cam.transform.position, target.transform.position, camMoveSpeed * Time.deltaTime);
    		yield return new WaitForEndOfFrame();
    	}
        yield return null;
    }
}
