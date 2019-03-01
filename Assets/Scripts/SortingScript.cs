using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<MeshRenderer>().sortingOrder = 20;
    }

    #if UNITY_EDITOR
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        this.gameObject.GetComponent<MeshRenderer>().sortingOrder = 20;
    }
    #endif
}
