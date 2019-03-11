using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(loadAfter());
    }

    IEnumerator loadAfter()
    {
        yield return new WaitForSeconds(3f);
        PlayerPrefs.SetString("cam", "main");
        SceneManager.LoadScene("Home");
    }
}
