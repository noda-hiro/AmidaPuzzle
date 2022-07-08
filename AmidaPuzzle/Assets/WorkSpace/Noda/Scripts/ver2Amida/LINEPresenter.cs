using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;

public class LINEPresenter : MonoBehaviour
{
    //public int lineNumber = 0;
    private int CreateCount = 0;
    private Vector3 startPos;
    private Vector3 endPos;
    private float maxDistance = 10f;
    CreateLines lineControl;
    private LineModel lineModel;
    private Dictionary<Vector3, Vector3> amidaLineList = new Dictionary<Vector3, Vector3>();
    [SerializeField]
    private Camera mainCamera;

    // Start is called before the first frame update
    void Awake()
    {
        lineControl = CreateLines.Instance;
        this.lineModel = new LineModel();

        OnMouseFirstPush();
        OnMouseUped();
        SuccessChangeBoolState();

    }

    /// <summary>
    /// �}�E�X�����߂ăN���b�N���ꂽ���̏���
    /// </summary>
    private void OnMouseFirstPush()
    {
        var mouseFirstDown = this.UpdateAsObservable()
            .Where(_ => Input.GetMouseButtonDown(0))        //�{�^���������ꂽ�� 
            .Select(_ => Camera.main.ScreenPointToRay(Input.mousePosition))//Camera���猩���}�E�X�|�C���g��
            .Subscribe(X =>
           {
               InputMousePosRay(X);
           })
            .AddTo(this);
    }

    private void InputMousePosRay(Ray X)
    {
        startPos = Vector3.zero;
        var hit = Physics2D.Raycast(X.origin, X.direction, maxDistance);

        if (hit == false)
        {
            this.lineModel.FailureCreateFirstLinePoint();
            return;
        }
        if (hit.collider.gameObject.tag == "verticalLine"
            && CreateCount < lineControl.NumberOfUnitsRequired)
        {
            CreateCount++;
            this.lineModel.SuccessCreateFirstLinePoint();
            startPos = X.origin;
            Debug.Log(startPos);
        }
    }

    private void SuccessChangeBoolState()
    {

        this.lineModel.LineCurrentTypeCount
            .Subscribe(_ =>
            {
                if (this.lineModel.LineCurrentTypeCount.Value == true)
                {
                  var a=  this.UpdateAsObservable()
                    .Subscribe(_ =>
                    this.lineModel.CreateLine(startPos, mainCamera, lineControl.linePrefab));
                }
                else if (this.lineModel.LineCurrentTypeCount.Value == false)
                {
                    Debug.Log("ss");
                }
                //destory����
            });
    }

    /// <summary>
    /// �{�^���������ꂽ���̏���
    /// </summary>
    private void OnMouseUped()
    {
        var mouseUp = this.UpdateAsObservable()
            .Where(_ => Input.GetMouseButtonUp(0))
            .Select(_ => Camera.main.ScreenPointToRay(Input.mousePosition))//Camera���猩���}�E�X�|�C���g��
             .Select(X =>
             {
                 endPos = X.origin;//Ray���΂��ă^�v���ŕϐ��������Ԃ�

                 var hit = Physics2D.Raycast(X.origin, X.direction, maxDistance);

                 var lineNumber = hit.collider.gameObject.GetComponent<LINE>();
                 return Tuple.Create(hit, lineNumber);
             })
            .Subscribe(x =>
            {
                if (x.Item1.collider.gameObject != null
                 // && x.Item2.LineNumber == lineNumber++
                 //|| x.Item2.LineNumber == lineNumber--
                 )
                {
                    amidaLineList.Add(startPos, endPos);
                    Debug.Log(endPos);
                    Debug.Log(amidaLineList.Values + "�n�_" + amidaLineList.Keys.Count + "�I�_");
                }
                else
                {
                    Debug.Log("aa" + x.Item1.collider.gameObject.name);
                    return;
                }
            })
            .AddTo(this);
    }

}
