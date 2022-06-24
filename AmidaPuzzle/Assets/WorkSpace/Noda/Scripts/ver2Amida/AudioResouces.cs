using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioResouces : MonoBehaviour
{
    [SerializeField] private AudioClip[] SeAudios;

    public AudioClip FindSeByName(string clipName)
    {
        return FindClipName(clipName, SeAudios);
    }

    public AudioClip FindClipName(string clipName, AudioClip[] audioClips)
    {
        if (audioClips == null) return null;

        AudioClip findClip = null;
        foreach(AudioClip clip in audioClips)
        {
            if (clip.name != clipName) continue;

            findClip = clip;
            break;
        }
        if(findClip == null)
        {
            Debug.LogError("éwñºÇµÇΩñºëOÇÃAudioClipÇÕå©Ç¬Ç©ÇËÇ‹ÇπÇÒÇ≈ÇµÇΩÅB");
        }
        return findClip;
    }
}
