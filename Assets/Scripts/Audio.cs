using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public AudioSource audio;
    public GameObject audioPrefab;

    public AudioClip slice;
    public AudioClip sword;
    public AudioClip spin;
    public AudioClip whoosh;
    public AudioClip blowUp;
    public AudioClip blowDown;
    public AudioClip gun;
    public AudioClip swordMiss;
    public AudioClip waterButton1;
    public AudioClip waterButton2;
    public AudioClip splashIn;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    public void sound(AudioClip clip, float volume, float pitch)
    {
        StartCoroutine(playSound(clip, volume, pitch));
    }

    public void soundLong(AudioClip clip, float volume, float pitch)
    {
        StartCoroutine(playSoundLong(clip, volume, pitch));
    }

    // random pitch
    public void sound(AudioClip clip, float volume, string rand)
    {
        if (rand == "random")
        {
            float random = Random.Range(0.9f, 1.05f);
            StartCoroutine(playSound(clip, volume, random));
        }
    }

    IEnumerator playSound(AudioClip clip, float volume, float pitch)
    {
        GameObject _audio = Instantiate(audioPrefab);
        audio = _audio.GetComponent<AudioSource>();
    	audio.volume = volume;
        audio.pitch = pitch;
        audio.PlayOneShot(clip);
        if(!audio.isPlaying)
        {
            Destroy(_audio);
        }
        yield return new WaitForSeconds(2f);
        Destroy(_audio);
    	yield return null;
    }

    IEnumerator playSoundLong(AudioClip clip, float volume, float pitch)
    {
        GameObject _audio = Instantiate(audioPrefab);
        audio = _audio.GetComponent<AudioSource>();
    	audio.volume = volume;
        audio.pitch = pitch;
        audio.clip = clip;
        audio.Play();
        if(!audio.isPlaying)
        {
            Destroy(_audio);
        }
        yield return new WaitForSeconds(0.4f);
        Destroy(_audio);
    	yield return null;
    }

}
