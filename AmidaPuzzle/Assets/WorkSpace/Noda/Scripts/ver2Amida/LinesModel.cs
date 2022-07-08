using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System.Linq;

public class LinesModel
{
    public IReactiveProperty<int> LineCurrentTypeCount { get; private set; }
    public IReactiveProperty<int> LineCurrentTypeCount2 { get; private set; }
    private Vector3 startPos;
    // private GameObject firstCollisionGameobject;
    private Vector3 endPos;
    private float maxDistance = 10f;
    private Vector3 screenToWorldPointPosition;
    private int firstPos = 0;
    private int secondPos = 1;
    private CreateLines createLine;
    private LineRenderer lineren;
    private int count = 0;
    Vector3 screenPosition;
    Vector3 mousePosition;
    LINE firstClickObjLineNum = null;

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

    public void Debugyou()
    {
        Debug.Log(this.LineCurrentTypeCount.Value);
        Debug.Log(this.LineCurrentTypeCount2.Value);
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
            //UpdateCheckActiveLine();
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
        // endPos = Vector3.zero;
        var hit = Physics2D.Raycast(X.origin, X.direction, maxDistance);
        LINE LINEScript = hit.collider.gameObject.GetComponent<LINE>();
       // if (LINEScript == null) return;
        Debug.Log(LINEScript.LineNumber + "lineNum");

        if (hit.collider.gameObject.tag == "BG"|| LINEScript.LineNumber == firstClickObjLineNum.LineNumber + 2 || LINEScript.LineNumber == firstClickObjLineNum.LineNumber - 2)
        {
            //   PosResset();
            FailureCreateSecondLinePoint();
            LineCurrentTypeCountNormal();
            LineCurrentTypeCountNormal2();
            Debug.Log("a");
        }
        else if (this.LineCurrentTypeCount.Value == 1 && LINEScript.LineNumber == firstClickObjLineNum.LineNumber++
            || this.LineCurrentTypeCount.Value == 1 && LINEScript.LineNumber == firstClickObjLineNum.LineNumber--)
        {
            SuccessCreateSecondLinePoint();

            LineCurrentTypeCountNormal();
            LineCurrentTypeCountNormal2();
            Debug.Log("b");
        }
        //else if (LINEScript.LineNumber == firstClickObjLineNum.LineNumber + 2 || LINEScript.LineNumber == firstClickObjLineNum.LineNumber - 2)
        //{
        //    //   PosResset();
        //    FailureCreateSecondLinePoint();
        //    LineCurrentTypeCountNormal();
        //    LineCurrentTypeCountNormal2();
        //    Debug.Log("a");
        //}
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

            screenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1.0f);
            mousePosition = Camera.main.ScreenToWorldPoint(screenPosition);
            yield return new WaitForSeconds(0.001f);
            lineren.SetPosition(secondPos, mousePosition);
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
        createLine.lineCreated.Add(lineren.gameObject);
        createLine.linePrefabs.Remove(lineren.gameObject);
        createLine.currentCount++;
        createLine.line = null;
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

}
