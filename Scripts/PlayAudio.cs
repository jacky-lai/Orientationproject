using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    //public AudioSource preNarration;
    bool preNarrationPlayed = true;
    public static float timer;

    // Start is called before the first frame update
    void Start()
    {
        
        //preNarration = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        timer = Time.time;

        if (timer > 3.00f & preNarrationPlayed)
        {
            GetComponent<AudioSource>().Play();
            preNarrationPlayed = false;

        }
    }


}