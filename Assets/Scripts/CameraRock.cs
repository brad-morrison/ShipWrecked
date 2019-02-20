using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRock : MonoBehaviour
{
    public float speed;

    public float leftMax, rightMax;
    Quaternion left;
    Quaternion right;

    // Start is called before the first frame update
    void Start()
    {
        Quaternion left = Quaternion.Euler(0,0,leftMax);
        Quaternion right = Quaternion.Euler(0,0,rightMax);
    }

    void Update()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, left, Time.time * speed);
    }
    
}
