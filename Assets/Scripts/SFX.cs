using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SFX : MonoBehaviour
{

    public AudioSource source;
    public AudioClip shootRockClip;
    public AudioClip shootScissorsClip;
    public AudioClip shootPaperClip;

    public void shootRock()
    {
        source.clip = shootRockClip;
        source.Play();
    }
    public void shootScissors()
    {
        source.clip = shootScissorsClip;
        source.Play();
    }

    public void shootPaper()
    {
        source.clip = shootPaperClip;
        source.Play();
    }

    //public List<AudioClip> clips = new List<AudioClip>();

}
