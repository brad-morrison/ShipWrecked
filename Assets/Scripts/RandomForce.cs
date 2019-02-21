using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomForce : MonoBehaviour
{
    public GameObject[] bits;

    public float thrust;
    
    
    // Start is called before the first frame update
    void Start()
    {
        int i;

        for (i = 0; i < bits.Length; i++)
        {
            bits[i].GetComponent<Rigidbody2D>().AddForce(transform.up * thrust);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        for (int i = 0; i < bits.Length; i++)
        {
            bits[i].GetComponent<Rigidbody2D>().AddForce(transform.up * thrust, ForceMode2D.Impulse);
        }
    }
}
