using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System.Linq;
using System;

public class LinesModel
{
    public IReactiveProperty<int> LineCurrentTypeCount { get; private set; }
    public IReactiveProperty<int> LineCurrentTypeCount2 { get; private set; }
    public IReactiveProperty<bool> isCross { get; private set; }
    private Vector3 startPos;
    // private GameObject firstCollisionGameobject;
    private Vector3 endPos;
    private float maxDistance = 2f;
    private Vector3 screenToWorldPointPosition;
    private int firstPos = 0;
    private int secondPos = 1;
    private CreateLines createLine;
    private LineRenderer lineren;
    private int count = 0;
    Vector3 screenPosition;
    Vector3 mousePosition;
    LINE firstClickObjLineNum = null;
    private float mouseDistance = 100f;
    private List<Vector2> firstPosList = new List<Vector2>();
    private List<Vector2> endPosList = new List<Vector2>();
    private List<List<Vector3>> AllPosList = new List<List<Vector3>>();
    private bool IsCross = false;

    public LinesModel()
    {
        this.LineCurrentTypeCount = new ReactiveProperty<int>();
        this.LineCurrentTypeCount2 = new ReactiveProperty<int>();
    }

    #region 一回目
    public void SuccessCreateFirstLinePoint()
    {
        this.LineCurrentTypeCount.Value = 1;
    }

    public void FailureCreateFirstLinePoint()
    {
        this.LineCurrentTypeCount.Value = -1;
    }
    #endregion

    #region 二回目
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
    /// クリックをした場所にRayを飛ばしLINEに当たった時と当たらなかったときのBOOL切り替え処理
    /// TRUE(LINEに当たった場合)(hit.collider.gameObject.tag == "verticalLine")：BOOLをTRUEにしSTARTPOS変数にヒットしたポジションを代入
    /// FALSE(LINEに当たらなかった場合)if (hit == false)：BOOLをFALSEに
    /// </summary>
    /// <param name="X">Rayポイント</param>
    public void InputMouseDownPosRay(Ray X, CreateLines create)
    {
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
            firstClickObjLineNum = hit.collider.gameObject.GetComponent<LINE>();

            create.Createline();
            lineren = create.line.GetComponent<LineRenderer>();
            screenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1f);
            mousePosition = Camera.main.ScreenToWorldPoint(screenPosition);
            SuccessCreateFirstLinePoint();
            startPos = mousePosition;
            lineren.SetPosition(firstPos, new Vector3(startPos.x, startPos.y, 1f));

        }
    }

    /// <summary>
    /// クリックをした場所にRayを飛ばしLINEに当たった時と当たらなかったときのBOOL切り替え処理
    /// TRUE(LINEに当たった場合)else if (LINEScript.LineNumber == LINEScript.LineNumber++
    /// || LINEScript.LineNumber == LINEScript.LineNumber--)：BOOLをTRUEにし
    /// FALSE(LINEに当たらなかった場合)(hit == false)：座標を全てリセット
    /// </summary>
    /// <param name="X">Rayポイント</param>
    public void InputMouseUpPosRay(Ray X)
    {
        var hit = Physics2D.Raycast(X.origin, X.direction, maxDistance);
        LINE LINEScript = hit.collider.gameObject.GetComponent<LINE>();
        var a = firstClickObjLineNum.LineNumber += 1;
        var b = firstClickObjLineNum.LineNumber -= 1;
        if (hit.collider.gameObject.tag == "BG" || LINEScript.LineNumber == firstClickObjLineNum.LineNumber + 2 || LINEScript.LineNumber == firstClickObjLineNum.LineNumber - 2)
        {
            //   PosResset();
            FailureCreateSecondLinePoint();
            LineCurrentTypeCountNormal();
            LineCurrentTypeCountNormal2();
        }
        else if (this.LineCurrentTypeCount.Value == 1 && LINEScript.LineNumber == a
            || this.LineCurrentTypeCount.Value == 1 && LINEScript.LineNumber == b)
        {
            SuccessCreateSecondLinePoint();
            LineCurrentTypeCountNormal();
            LineCurrentTypeCountNormal2();
        }
        else
        {
            SuccessCreateSecondLinePoint();
            LineCurrentTypeCountNormal();
            LineCurrentTypeCountNormal2();
        }
    }

    /// <summary>
    /// クリックが成功した売位LINERENDEREの位置をマウスの現在の位置にする
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
            //ここにマウスを追うオブジェクトを若干Distanceを用意する　+*+ -*+
            screenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1.0f);
            mousePosition = Camera.main.ScreenToWorldPoint(screenPosition);
            //Debug.LogError(mousePosition.normalized.normalized.x + "ふぉわーど");
            Debug.LogError(lineren.GetPosition(1).normalized.x);
            lineren.SetPosition(secondPos, new Vector2(mousePosition.x - (startPos.normalized.x * mousePosition.normalized.x - maxDistance), mousePosition.y));
            yield return null;
        }
    }

    /// <summary>
    /// 作成成功時
    /// </summary>
    /// <param name="line"></param>
    public void SuccessCreate()
    {
        screenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1.0f);
        mousePosition = Camera.main.ScreenToWorldPoint(screenPosition);
        lineren.SetPosition(secondPos, mousePosition);
        lineren.GetComponent<VerticalLine>().isComprete = true;


        firstPosList.Add(lineren.GetPosition(0));
        endPosList.Add(lineren.GetPosition(1));

        for (int i = 0; i < endPosList.Count; i++)
        {
            var isCrossLine = IsCrossing(firstPosList[i], endPosList[i], lineren.GetPosition(0), lineren.GetPosition(1));
            IsCross = isCrossLine;
            Debug.Log(isCrossLine);
            if (IsCross == true)
            {
               // firstPosList.Remove(lineren.GetPosition(0));
                //endPosList.Remove(lineren.GetPosition(1));
                Debug.Log(firstPosList.Count);
                Debug.Log(firstPosList.Count);
                FailureCreateSecondLinePoint();
                LineCurrentTypeCountNormal();
                LineCurrentTypeCountNormal2();
                break;
            }
        }
        if (IsCross == false)
        {

            Debug.Log("false");
            createLine.lineCreated.Add(lineren.gameObject);
            createLine.linePrefabs.Remove(lineren.gameObject);
            createLine.currentCount++;
            createLine.line = null;
        }
        //createLine.lineCreated.Add(lineren.gameObject);
        //createLine.linePrefabs.Remove(lineren.gameObject);
        //createLine.currentCount++;
        //createLine.line = null;
    }


    public void Init()
    {
        createLine = CreateLines.Instance;
    }

    /// <summary>
    /// 座標リセット
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
