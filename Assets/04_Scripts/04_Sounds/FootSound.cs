using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSound : MonoBehaviour
{
    public AudioClip[] footSoundClip;
    private AudioSource audioSource;
    private Rigidbody rg;
    public float footSoundTheresHold;
    public float footSoundRate;
    private float footSoundTime;

    // Start is called before the first frame update
    void Start()
    {
        rg = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(rg.velocity.y) < 0.1f)
        {
            if (rg.velocity.magnitude > footSoundTheresHold)
            {
                if (Time.time - footSoundTime > footSoundRate)
                {
                    footSoundTime = Time.time;
                    audioSource.PlayOneShot(footSoundClip[Random.Range(0, footSoundClip.Length)]);
                }
            }
        }
    }
}
