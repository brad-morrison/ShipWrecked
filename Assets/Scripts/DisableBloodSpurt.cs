using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableBloodSpurt : MonoBehaviour
{
    public GameObject victim;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void disable()
    {
        Destroy(victim);
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
