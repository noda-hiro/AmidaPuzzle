using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class AudioTest : ScriptableObject
{

   // AudioSourceをScriptableObjectに登録して、
     // 登録したAudioSourceのPlayOneShotを呼び出すScriptableObjec
    public void PlayOneShot(AudioClip clip)
    {
        if(audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    public AudioSource audioSource { get; set; }
}
