using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class AudioTest : ScriptableObject
{
    public void PlayOneShot(AudioClip clip)
    {
        if(audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    public AudioSource audioSource { get; set; }
}
