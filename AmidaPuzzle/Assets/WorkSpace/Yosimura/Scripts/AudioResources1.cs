using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioResources : MonoBehaviour
{
    [SerializeField] private AudioClip[] SeAudios;
   
    public AudioClip FindSeByName(string clipName)
    {
        return FindClipName(clipName, SeAudios);
    }
    /// <summary>
    /// se‚ð“n‚³‚ê‚½–¼‘O‚©‚ç“n‚·
    /// </summary>
    /// <param name="clipName"></param>
    /// <param name="audioClips"></param>
    /// <returns></returns>
    public AudioClip FindClipName(string clipName, AudioClip[] audioClips)
    {
        if (audioClips == null) return null;

        AudioClip findClip = null;
        foreach (AudioClip clip in audioClips)
        {
            if (clip.name != clipName) continue;

            findClip = clip;
            break;
        }
        if (findClip == null)
        {
            Debug.LogError("Žw’è‚µ‚½–¼‘O‚ÌAudioClip‚ÍŒ©‚Â‚©‚è‚Ü‚¹‚ñ‚Å‚µ‚½" + clipName);
        }
        return findClip;
    }
}
