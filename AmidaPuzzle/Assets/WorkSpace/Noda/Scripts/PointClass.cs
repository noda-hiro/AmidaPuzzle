using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum GameStateType
{
    STARTPOS,
    ENDPOS,
    MOVEPOS,
}


public class PointClass : MonoBehaviour
{
    public int PointNumber { get; private set; }
    private Transform _currentPos = null;
    public Vector3 _currentPoint;
    public Vector3 _onePoint;
    public Vector3 _twoPoint;
    public Transform EndPos { get; set; }
    // public Action BlockInit

    public void Init(int num)
    {
        PointNumber = num;
        Debug.LogError(PointNumber);
    }

    private void Awake()
    {
        _currentPos = transform;
        // SettingEndPos();
    }

    public float GetDistance(Transform pos)
    {
        return (pos.localPosition - _currentPos.localPosition).magnitude;
    }


    private void SettingEndPos()
    {
        switch (PointNumber)
        {
            case 1:
                EndPos = GameObject.Find("Firstline_EndPos").gameObject.GetComponent<Transform>();
                break;
            case 2:
                EndPos = GameObject.Find("Secondline_EndPos").gameObject.GetComponent<Transform>();
                break;
            case 3:
                Debug.Log("èÄîıíÜ");
                break;
        }
    }
}
