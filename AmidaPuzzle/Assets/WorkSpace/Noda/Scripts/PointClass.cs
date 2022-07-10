using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointClass : MonoBehaviour
{
    public int laneNumber;
    private Transform _currentPos = null;
    public PointClass _currentPoint;
    public PointClass _onePoint;
    public PointClass _twoPoint;
    public Transform EndPos { get; set; }
    // public Action BlockInit


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
        switch (laneNumber)
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
