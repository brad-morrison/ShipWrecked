using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingScript : MonoBehaviour
{
    public bool isText;

    // Start is called before the first frame update
    void Start()
    {
        if (isText)
        {

        }
        else
        {
            this.gameObject.GetComponent<MeshRenderer>().sortingOrder = 500;
        }
        
    }

    #if UNITY_EDITOR
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        /*if (isText)
        {

        }
        else
        {
            this.gameObject.GetComponent<MeshRenderer>().sortingOrder = 20;
        }*/
    }
    #endif
}
