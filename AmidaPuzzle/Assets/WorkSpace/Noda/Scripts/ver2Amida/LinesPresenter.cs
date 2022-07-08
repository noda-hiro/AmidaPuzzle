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
    /// �}�E�X�����߂ăN���b�N���ꂽ���̏���
    /// </summary>
    private void OnMouseFirstPush()
    {
        var mouseFirstDown = this.UpdateAsObservable()
            .Where(_ => Input.GetMouseButtonDown(0))        //�{�^���������ꂽ�� 
            .Select(_ => Camera.main.ScreenPointToRay(Input.mousePosition))//Camera���猩���}�E�X�|�C���g��
            .Subscribe(X =>
            {
                lineModel.InputMouseDownPosRay(X,create);
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
}
