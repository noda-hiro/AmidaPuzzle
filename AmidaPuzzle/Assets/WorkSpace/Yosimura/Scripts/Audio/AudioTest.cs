using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class AudioTest : ScriptableObject
{

   // AudioSource‚ğScriptableObject‚É“o˜^‚µ‚ÄA
     // “o˜^‚µ‚½AudioSource‚ÌPlayOneShot‚ğŒÄ‚Ño‚·ScriptableObjec
    public void PlayOneShot(AudioClip clip)
    {
        if(audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    public AudioSource audioSource { get; set; }
}
