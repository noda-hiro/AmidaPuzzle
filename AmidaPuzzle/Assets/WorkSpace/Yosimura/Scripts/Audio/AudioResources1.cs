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
    /// seを渡された名前から渡す
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
            Debug.LogError("指定した名前のAudioClipは見つかりませんでした" + clipName);
        }
        return findClip;
    }
}
