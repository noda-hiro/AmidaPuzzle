using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockActionListener : MonoBehaviour
{
    public Action OnCollisionBlockListener;

    public void OnCollisionBlock()
    {
        if(OnCollisionBlockListener!=null)
        {
            OnCollisionBlockListener();
        }
    }
}
