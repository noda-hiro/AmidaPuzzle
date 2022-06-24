using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SingletonMonobehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                Type t = typeof(T);

                instance = (T)FindObjectOfType(t);
                if (instance == null)
                {
                    Debug.LogError(t + "をアタッチしているGameObjectはありません");
                }
            }
            return instance;
        }
    }
    virtual protected void Awake()
    {
        if (this != Instance)
        {
            Destroy(this);
            Debug.LogError(
                typeof(T) +
                "はすでに他のGameObjectにアタッチされているため、コンポーネントを放棄しました。" +
                "アタッチされているGameObjectは" + Instance.gameObject.name + "です");
            return;
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
