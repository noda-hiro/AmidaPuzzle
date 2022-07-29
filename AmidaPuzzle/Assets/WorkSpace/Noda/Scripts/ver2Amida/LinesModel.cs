using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class LinesModel
{
    public IReactiveProperty<int> LineCurrentTypeCount { get; private set; }
    public IReactiveProperty<int> LineCurrentTypeCount2 { get; private set; }
    public IReactiveProperty<bool> isCross { get; private set; }
    public Vector3 startPos;
    public Vector3 endPos;
    private float maxDistance = 0f;
    private Vector3 screenToWorldPointPosition;
    private int firstPos = 0;
    private int secondPos = 1;
    private CreateLines createLine;
    private LineRenderer lineren;
    private int count = 0;
    Vector3 screenPosition;
    Vector3 mousePosition;
    public LINE firstClickObjLineNum = null;
    public LINE endClickObjLineNum = null;
    private float mouseDistance = 100f;
    private List<Vector2> firstPosList = new List<Vector2>();
    private List<Vector2> endPosList = new List<Vector2>();
    private List<List<Vector3>> AllPosList = new List<List<Vector3>>();
    private bool IsCross = false;
    public GameObject firstClickLineObj = null;
    public GameObject secondClickLineObj = null;

    public LinesModel()
    {
        this.LineCurrentTypeCount = new ReactiveProperty<int>();
        this.LineCurrentTypeCount2 = new ReactiveProperty<int>();
    }

    #region ����
    public void SuccessCreateFirstLinePoint()
    {
        this.LineCurrentTypeCount.Value = 1;
    }

    public void FailureCreateFirstLinePoint()
    {
        this.LineCurrentTypeCount.Value = -1;
    }
    #endregion

    #region ����
    public void SuccessCreateSecondLinePoint()
    {
        this.LineCurrentTypeCount2.Value = 1;
    }

    public void FailureCreateSecondLinePoint()
    {
        this.LineCurrentTypeCount2.Value = -1;
    }

    public void LineCurrentTypeCountNormal()
    {
        this.LineCurrentTypeCount.Value = 0;
    }

    public void LineCurrentTypeCountNormal2()
    {
        this.LineCurrentTypeCount2.Value = 0;
    }

    #endregion

    /// <summary>
    /// �N���b�N�������ꏊ��Ray���΂�LINE�ɓ����������Ɠ�����Ȃ������Ƃ���BOOL�؂�ւ�����
    /// TRUE(LINE�ɓ��������ꍇ)(hit.collider.gameObject.tag == "verticalLine")�FBOOL��TRUE�ɂ�STARTPOS�ϐ��Ƀq�b�g�����|�W�V��������
    /// FALSE(LINE�ɓ�����Ȃ������ꍇ)if (hit == false)�FBOOL��FALSE��
    /// </summary>
    /// <param name="X">Ray�|�C���g</param>
    public void InputMouseDownPosRay(Ray X, CreateLines create)
    {
        firstClickLineObj = null;
        PosResset();

        var hit = Physics2D.Raycast(X.origin, X.direction, maxDistance);

        if (hit == false)
        {
            FailureCreateFirstLinePoint();
            LineCurrentTypeCountNormal();
            LineCurrentTypeCountNormal2();
        }
        else if (hit.collider.gameObject.tag == "verticalLine")
        {
            firstClickLineObj = hit.collider.gameObject;
            firstClickObjLineNum = hit.collider.gameObject.GetComponent<LINE>();
            create.Createline();
            lineren = create.line.GetComponent<LineRenderer>();
            screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            mousePosition = Camera.main.ScreenToWorldPoint(screenPosition);
            SuccessCreateFirstLinePoint();
            startPos = mousePosition;
            lineren.SetPosition(firstPos, startPos);
        }
    }

    /// <summary>
    /// �N���b�N�������ꏊ��Ray���΂�LINE�ɓ����������Ɠ�����Ȃ������Ƃ���BOOL�؂�ւ�����
    /// TRUE(LINE�ɓ��������ꍇ)else if (LINEScript.LineNumber == LINEScript.LineNumber++
    /// || LINEScript.LineNumber == LINEScript.LineNumber--)�FBOOL��TRUE�ɂ�
    /// FALSE(LINE�ɓ�����Ȃ������ꍇ)(hit == false)�F���W��S�ă��Z�b�g
    /// </summary>
    /// <param name="X">Ray�|�C���g</param>
    public void InputMouseUpPosRay(Ray X)
    {
        secondClickLineObj = null;
        var hit = Physics2D.Raycast(X.origin, X.direction, maxDistance);
        LINE LINEScript = hit.collider.gameObject.GetComponent<LINE>();
        var a = firstClickObjLineNum.LineNumber++;
        var b = firstClickObjLineNum.LineNumber--;
        if (hit.collider.gameObject.tag == "BG" || LINEScript.LineNumber == firstClickObjLineNum.LineNumber + 2
            || LINEScript.LineNumber == firstClickObjLineNum.LineNumber - 2
            || LINEScript.LineNumber == firstClickObjLineNum.LineNumber)
        {
            //   PosResset();
            FailureCreateSecondLinePoint();
            LineCurrentTypeCountNormal();
            LineCurrentTypeCountNormal2();
        }
        else if (this.LineCurrentTypeCount.Value == 1 && LINEScript.LineNumber == a
            || this.LineCurrentTypeCount.Value == 1 && LINEScript.LineNumber == b)
        {
            endClickObjLineNum = hit.collider.gameObject.GetComponent<LINE>();
            secondClickLineObj = hit.collider.gameObject;
            SuccessCreateSecondLinePoint();
            LineCurrentTypeCountNormal();
            LineCurrentTypeCountNormal2();
        }
        else
        {
            endClickObjLineNum = hit.collider.gameObject.GetComponent<LINE>();
            secondClickLineObj = hit.collider.gameObject;
            SuccessCreateSecondLinePoint();
            LineCurrentTypeCountNormal();
            LineCurrentTypeCountNormal2();
        }
    }

    /// <summary>
    /// �N���b�N��������������LINERENDERE�̈ʒu���}�E�X�̌��݂̈ʒu�ɂ���
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="camera"></param>
    /// <param name="line"></param>
    public IEnumerator CreateLine()
    {
        while (true)
        {
            if (this.LineCurrentTypeCount.Value != 1 || Input.GetMouseButtonUp(0))
            {
                LineCurrentTypeCountNormal();
                LineCurrentTypeCountNormal2();
                yield break;
            }
            //�����Ƀ}�E�X��ǂ��I�u�W�F�N�g���኱Distance��p�ӂ���@+*+ -*+
            screenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1.0f);
            mousePosition = Camera.main.ScreenToWorldPoint(screenPosition);
            var dir = (mousePosition - startPos).normalized;
            lineren.SetPosition(secondPos, (mousePosition - (dir * maxDistance)));
            yield return null;
        }
    }

    /// <summary>
    /// �쐬������
    /// </summary>
    /// <param name="line"></param>
    public void SuccessCreate()
    {
        screenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1.0f);
        mousePosition = Camera.main.ScreenToWorldPoint(screenPosition);
        lineren.SetPosition(secondPos, mousePosition);
        lineren.GetComponent<VerticalLine>().isComprete = true;

        endPos = mousePosition;
        firstPosList.Add(lineren.GetPosition(0));
        endPosList.Add(lineren.GetPosition(1));

        for (int i = 0; i < endPosList.Count; i++)
        {
            var isCrossLine = IsCrossing(firstPosList[i], endPosList[i], lineren.GetPosition(0), lineren.GetPosition(1));
            IsCross = isCrossLine;
            if (IsCross == true)
            {
                FailureCreateSecondLinePoint();
                LineCurrentTypeCountNormal();
                LineCurrentTypeCountNormal2();
                var firstPosItem = firstPosList.LastIndexOf(firstPosList[i]);
                var secondPosItem = endPosList.LastIndexOf(endPosList[i]);
                break;
            }
        }
        if (IsCross == false)
        {
            createLine.lineCreated.Add(lineren.gameObject);
            createLine.linePrefabs.Remove(lineren.gameObject);
            createLine.currentCount++;
            createLine.line = null;
        }
    }

    public void Init()
    {
        createLine = CreateLines.Instance;
    }

    /// <summary>
    /// ���W���Z�b�g
    /// </summary>
    public void PosResset()
    {
        startPos = Vector2.zero;
        endPos = Vector2.zero;
        screenPosition = Vector2.zero;
        mousePosition = Vector2.zero;
    }

    private bool IsCrossing(Vector2 startPoint1, Vector2 endPoint1, Vector2 startPoint2, Vector2 endPoint2)
    {
        var vector1 = endPoint1 - startPoint1;
        var vector2 = endPoint2 - startPoint2;

        return Cross(vector1, startPoint2 - startPoint1) * Cross(vector1, endPoint2 - startPoint1) < 0 &&
               Cross(vector2, startPoint1 - startPoint2) * Cross(vector2, endPoint1 - startPoint2) < 0;
    }

    private float Cross(Vector2 vector1, Vector2 vector2)
        => vector1.x * vector2.y - vector1.y * vector2.x;

}
