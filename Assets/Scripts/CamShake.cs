using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamShake : MonoBehaviour
{
	public float shakeSpeed;
	public float shakeAmountx;
	public float shakeAmounty;
	public bool shakeOn;

	Vector2 startingPos = new Vector2(0,0);

    public void shake(float time)
    {
    	shakeOn = true;
    	startingPos.x = transform.position.x;
        startingPos.y = transform.position.y;
    	StartCoroutine(shakeOff(time));
    }

    void Start()
    {
    	startingPos.x = transform.position.x;
        startingPos.y = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = new Vector3(0,0,-10);

    	
    	if (shakeOn)
    	{
    		position.y = startingPos.y + (Mathf.Sin(Time.time * shakeSpeed) * shakeAmounty);
    		position.x = startingPos.x + (Mathf.Sin(Time.time * shakeSpeed) * shakeAmountx);
    		transform.position = position;
    	}
    }

    IEnumerator shakeOff(float time)
    {
    	yield return new WaitForSeconds(time);
    	shakeOn = false;
    }
}
