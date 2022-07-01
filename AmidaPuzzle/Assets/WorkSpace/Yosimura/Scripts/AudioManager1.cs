using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager //: SingletonMonoBehaviour<AudioManager>
{
    [SerializeField] private AudioResources audioResources = null;
    private AudioSource audioSource = null;
    private string currentSe = null;
    private void Start()
    {
       // audioSource = GetComponent<AudioSource>();
    }
    /// <summary>
    /// ‰¹‚ð–Â‚ç‚·
    /// </summary>
    /// <param name="seName"></param>
    public void PlayStart(string seName)
    {
        AudioClip playSe = audioResources.FindSeByName(seName);
        if (playSe != null)
        {
            PlaySE(playSe);
            currentSe = seName;
        }
    }
    private void PlaySE(AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }
    public void PlayStop(string seName)
    {
        if (currentSe != seName) return;
        audioSource.Stop();
    }
}
