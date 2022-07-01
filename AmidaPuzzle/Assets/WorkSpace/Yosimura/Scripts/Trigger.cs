using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{

    public UnityEvent onAwake = new UnityEvent();
    public UnityEvent onDestroy = new UnityEvent();

   // Awake��Destroy�̃^�C�~���O�œo�^�������\�b�h���Ăяo��
    private void Awake()
    {
        onAwake.Invoke();
    }

    private void OnDestroy()
    {
        onDestroy.Invoke();
    }
}
