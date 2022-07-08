using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class LineModel
{
    public IReactiveProperty<bool> LineCurrentTypeCount { get; private set; }
    private Vector3 screenToWorldPointPosition;
    private int vecFirstPos = 0;
    private int currentPos = 1;

    public LineModel()
    {
        this.LineCurrentTypeCount = new ReactiveProperty<bool>();
    }

    public void SuccessCreateFirstLinePoint()
    {
        this.LineCurrentTypeCount.Value = true;
    }

    public void FailureCreateFirstLinePoint()
    {
        this.LineCurrentTypeCount.Value = false;
    }

    public void SuccessCreateSecondLinePoint()
    {
        this.LineCurrentTypeCount.Value = true;
    }

    public void FailureCreateSecondLinePoint()
    {
        this.LineCurrentTypeCount.Value = false;
    }

    public void CreateLine(Vector3 pos, Camera camera, GameObject line)
    {
        var mousePos = Input.mousePosition;
        var lineren = line.GetComponent<LineRenderer>();
        lineren.SetPosition(vecFirstPos, pos);
        mousePos.z = 10f;
        mousePos = pos;
        mousePos = camera.WorldToScreenPoint(mousePos);
        Debug.Log(mousePos);
        screenToWorldPointPosition = camera.ScreenToWorldPoint(mousePos);
        line.transform.position = screenToWorldPointPosition;
        lineren.SetPosition(currentPos, screenToWorldPointPosition);
    }

}
