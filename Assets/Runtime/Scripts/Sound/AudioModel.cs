using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioModel : MonoBehaviour
{
    [SerializeField] private AudioClip clip5;
    [SerializeField] private AudioClip clip3;
    [SerializeField] private AudioClip clip1;

    public void SetClip(int num)
    {
        AudioSource audioSource = GetComponent<AudioSource>();


        switch (num)
        {
            case 1: 
                audioSource.clip = clip1;
                break;
            case 3:
                audioSource.clip = clip3;
                break;
            case 5:
                audioSource.clip = clip5;
                break;
        }

        audioSource.Play();
    }
}
