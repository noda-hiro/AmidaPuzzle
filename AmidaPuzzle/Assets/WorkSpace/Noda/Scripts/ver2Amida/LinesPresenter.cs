using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using UnityEngine.UI;
using System;

public class LinesPresenter : MonoBehaviour
{
    private LinesModel lineModel;
    [SerializeField] CreateLines create;
    GameObject startposline;
    [SerializeField] GameObject linePoint;
    private int lineCount = 0;
    [SerializeField] private List<Transform> endPosList = new List<Transform>();
    [SerializeField] private List<BLOCK> Blocks = new List<BLOCK>();
    [SerializeField] private Button startBtn = null;

    private void Awake()
    {
        this.lineModel = new LinesModel();
    }

    void Start()
    {
        this.lineModel.PosResset();
        this.lineModel.Init();
        OnMouseFirstPush();
        OnMouseSecondPush();
        FirstJudgmentChangeBoolState();
        SecondJudgmentChangeBoolState();
        startBtn.onClick.AddListener(() => BlockInit());
      //  BlockInit();
    }

    /// <summary>
    /// �}�E�X�����߂ăN���b�N���ꂽ���̏���
    /// </summary>
    private void OnMouseFirstPush()
    {
        var mouseFirstDown = this.UpdateAsObservable()
            .Where(_ => Input.GetMouseButtonDown(0))
            .ThrottleFirst(TimeSpan.FromSeconds(1f))//�{�^���������ꂽ�� 
            .Select(_ => Camera.main.ScreenPointToRay(Input.mousePosition))//Camera���猩���}�E�X�|�C���g��
            .Subscribe(X =>
            {
                lineModel.InputMouseDownPosRay(X, create);
            })
            .AddTo(this);
    }

    /// <summary>
    /// �N���b�N�𗣂����Ƃ��̏���
    /// </summary>
    private void OnMouseSecondPush()
    {
        var mouseFirstDown = this.UpdateAsObservable()
            .Where(_ => Input.GetMouseButtonUp(0))        //�{�^���������ꂽ�� 
            .Select(_ => Camera.main.ScreenPointToRay(Input.mousePosition))//Camera���猩���}�E�X�|�C���g��
            .Subscribe(X =>
            {
                lineModel.InputMouseUpPosRay(X);
            })
            .AddTo(this);
    }

    ///�N���b�N�𗣂����Ƃ��̔��菈��
    private void FirstJudgmentChangeBoolState()
    {
        this.lineModel.LineCurrentTypeCount
            .Subscribe(_ =>
            {
                //�N���b�N�𗣂����Ƃ�LINE�̏�Ȃ琬��
                if (this.lineModel.LineCurrentTypeCount.Value == 1)
                {
                    StartCoroutine(this.lineModel.CreateLine());
                }
                //�N���b�N�𗣂����Ƃ�LINE�̏�ł͂Ȃ��ꍇ���s
                else if (this.lineModel.LineCurrentTypeCount.Value == -1)
                {
                    StopCoroutine(this.lineModel.CreateLine());
                    Destroy(create.line);
                    // this.lineModel.PosResset();
                }
            });
    }

    private void SecondJudgmentChangeBoolState()
    {
        this.lineModel.LineCurrentTypeCount2
            .Subscribe(_ =>
            {
                //�N���b�N�𗣂����Ƃ�LINE�̏�Ȃ琬��
                if (this.lineModel.LineCurrentTypeCount2.Value == 1)
                {
                    this.lineModel.SuccessCreate();
                    LinePointObjectInstance();
                }
                //�N���b�N�𗣂����Ƃ�LINE�̏�ł͂Ȃ��ꍇ���s
                else if (this.lineModel.LineCurrentTypeCount2.Value == -1)
                {
                    StopCoroutine(this.lineModel.CreateLine());
                    Destroy(create.line);
                    //   this.lineModel.PosResset();
                }
            });
    }
    private void LinePointObjectInstance()
    {
        startposline = Instantiate(linePoint, this.lineModel.startPos, Quaternion.identity);
        var endposline = Instantiate(linePoint, this.lineModel.endPos, Quaternion.identity);
        var endPoint = endposline.GetComponent<PointClass>();
        var startPoint = startposline.GetComponent<PointClass>();

        startPoint._onePoint = endPoint.transform.position;
        startPoint._currentPoint = startPoint.transform.position;
        endPoint._currentPoint = endPoint.transform.position;
        endPoint._onePoint = startPoint.transform.position;
        startPoint._twoPoint = endPosList[this.lineModel.firstClickObjLineNum.LineNumber].position;
        endPoint._twoPoint = endPosList[this.lineModel.endClickObjLineNum.LineNumber].position;
        Debug.Log(endPosList[this.lineModel.firstClickObjLineNum.LineNumber]+"�n�܂�");
        Debug.Log(endPosList[this.lineModel.endClickObjLineNum.LineNumber]+"�I���");
        startPoint.Init(lineCount);
        lineCount++;
        endPoint.Init(lineCount);
        lineCount++;
    }

    private void BlockInit()
    {
        for (int i = 0; i < Blocks.Count; i++)
        {
            Blocks[i].GetComponent<BLOCK>();
            Blocks[i].StartCoroutine(Blocks[i].MoveToDestinationPoint(endPosList[i].position,0,Blocks[i].isSwitching));
        }
    }

}
