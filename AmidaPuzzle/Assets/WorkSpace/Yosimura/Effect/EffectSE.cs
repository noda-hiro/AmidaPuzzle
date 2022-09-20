using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSE : MonoBehaviour
{
    [SerializeField]
    private GameObject explodeAudioObj;
    private ParticleSystem seedPs;

    // Use this for initialization
    void Start()
    {
        seedPs = GetComponent<ParticleSystem>();
        StartCoroutine(ProgressCo());
    }

    private int getSubEmitterParticleNum()
    {
        int ptNum = 0;
        ParticleSystem[] psArr = GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem ps in psArr)
        {
            ptNum += ps.particleCount;
        }
        return ptNum;
    }

    public IEnumerator ProgressCo()
    {
        // îöî≠ë“Çø
        while (getSubEmitterParticleNum() == 0)
        {
            yield return null;
        }
        // îöî≠âπ
        explodeAudioObj.GetComponent<AudioSource>().pitch *= Random.Range(0.8f, 1.2f);
        explodeAudioObj.GetComponent<AudioSource>().Play();
        //yield return new WaitForSeconds(1f);
        // è¡ñ≈ë“Çø
        while (getSubEmitterParticleNum() > 0)
        {
            yield return null;
        }
        // è¡ñ≈
        Destroy(gameObject);
    }
}


