using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace noda
{

    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioResouces audioResouces = null;
        private AudioSource audioSource = null;
        private string currentSe = null;

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public void PlayStart(string seName)
        {
            AudioClip playSe = audioResouces.FindSeByName(seName);

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
}
