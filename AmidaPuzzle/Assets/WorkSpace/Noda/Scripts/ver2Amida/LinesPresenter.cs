using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System.Threading.Tasks;

public class LinesPresenter : MonoBehaviour
{
    private LinesModel lineModel;
    [SerializeField] CreateLines create;

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

        this.UpdateAsObservable().Subscribe(_ =>
        {
            this.lineModel.Debugyou();


        });
    }

    /// <summary>
    /// マウスが初めてクリックされた時の処理
    /// </summary>
    private void OnMouseFirstPush()
    {
        var mouseFirstDown = this.UpdateAsObservable()
            .Where(_ => Input.GetMouseButtonDown(0))        //ボタンが押されたら 
            .Select(_ => Camera.main.ScreenPointToRay(Input.mousePosition))//Cameraから見たマウスポイントに
            .Subscribe(X =>
            {
                lineModel.InputMouseDownPosRay(X,create);
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
                    this.lineModel.SuccessCreate();
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
}
