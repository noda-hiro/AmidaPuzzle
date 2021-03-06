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
    [SerializeField] private Button resetButton = null;
    private void Awake()
    {
        this.lineModel = new LinesModel();
    }

    void Start()
    {
        startBtn.interactable = false;
        this.lineModel.PosResset();
        this.lineModel.Init();
        OnMouseFirstPush();
        OnMouseSecondPush();
        FirstJudgmentChangeBoolState();
        SecondJudgmentChangeBoolState();
        startBtn.onClick.AddListener(() => BlockInit());
        //  BlockInit();
        resetButton.onClick.AddListener(() => ResetLine());
    }

    /// <summary>
    /// マウスが初めてクリックされた時の処理
    /// </summary>
    private void OnMouseFirstPush()
    {
        this.lineModel.Init();
        var mouseFirstDown = this.UpdateAsObservable()
            .Where(_ => Input.GetMouseButtonDown(0))
            //.ThrottleFirst(TimeSpan.FromSeconds(1f))//ボタンが押されたら 
            .Select(_ => Camera.main.ScreenPointToRay(Input.mousePosition))//Cameraから見たマウスポイントに
            .Subscribe(X =>
            {
                lineModel.InputMouseDownPosRay(X, create);
            })
            .AddTo(this);
    }

    /// <summary>
    /// クリックを離したときの処理
    /// </summary>
    private void OnMouseSecondPush()
    {
        var mouseFirstDown = this.UpdateAsObservable()
            .Where(_ => Input.GetMouseButtonUp(0))        //ボタンが押されたら 
            .Select(_ => Camera.main.ScreenPointToRay(Input.mousePosition))//Cameraから見たマウスポイントに
            .Subscribe(X =>
            {
                lineModel.InputMouseUpPosRay(X);
            })
            .AddTo(this);
    }

    ///クリックを離したときの判定処理
    private void FirstJudgmentChangeBoolState()
    {
        this.lineModel.LineCurrentTypeCount
            .Subscribe(_ =>
            {
                //クリックを離したときLINEの上なら成功
                if (this.lineModel.LineCurrentTypeCount.Value == 1)
                {
                    StartCoroutine(this.lineModel.CreateLine());
                }
                //クリックを離したときLINEの上ではない場合失敗
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
                //クリックを離したときLINEの上なら成功
                if (this.lineModel.LineCurrentTypeCount2.Value == 1)
                {
                    create.UpdateLineCountText();
                    this.lineModel.SuccessCreate();
                    LinePointObjectInstance();
                    if (create.currentCount == create.maxCount)
                    {
                        startBtn.interactable = true;
                    }
                }
                //クリックを離したときLINEの上ではない場合失敗
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
        startposline.transform.parent = this.lineModel.firstClickLineObj.gameObject.transform;
        endposline.transform.parent = this.lineModel.secondClickLineObj.gameObject.transform;
        startposline.transform.localPosition = new Vector3(0f, startposline.transform.localPosition.y, startposline.transform.localPosition.z);
        endposline.transform.localPosition = new Vector3(0f, endposline.transform.localPosition.y, endposline.transform.localPosition.z);
        var endPoint = endposline.GetComponent<PointClass>();
        var startPoint = startposline.GetComponent<PointClass>();

        startPoint._onePoint = endPoint.transform.position;
        startPoint._currentPoint = startPoint.transform.position;
        endPoint._currentPoint = endPoint.transform.position;
        endPoint._onePoint = startPoint.transform.position;
        startPoint._twoPoint = endPosList[this.lineModel.firstClickObjLineNum.LineNumber].position;
        endPoint._twoPoint = endPosList[this.lineModel.endClickObjLineNum.LineNumber].position;
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
            Blocks[i].StartCoroutine(Blocks[i].MoveToDestinationPoint(endPosList[i].position, 0, Blocks[i].isSwitching));
        }
        startBtn.interactable = false;
    }

    public void ResetLine()
    {
        //  this.lineModel.PosResset();
        create.currentCount = create.maxCount;
        create.lineCount = create.maxCount;
        create.lineCreated.Clear();

        GameObject[] linePoint = GameObject.FindGameObjectsWithTag("LINEPOINT");
        GameObject[] linePrefab = GameObject.FindGameObjectsWithTag("AMIDA");

        foreach (GameObject point in linePoint)
        {
            Destroy(point);
        }
        foreach (GameObject line in linePrefab)
        {
            Destroy(line);
        }
    }
}
