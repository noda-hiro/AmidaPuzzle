using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private Camera mainCamera;
    private bool isTouch = true;
    private Vector3 pos;
    private float maxDistance = 1f;
    private bool IsCollision = false;
    private Transform currentPos;
    private Transform endPos;
    private LineManager lineManager;

    // Start is called before the first frame update
    void Start()
    {
        SettingLineRender();
    }

    // Update is called once per frame
    void Update()
    {
      LineContraction();
    }

    /// <summary>
    /// lineの初期設定
    /// </summary>
    private void SettingLineRender()
    {
        lineManager = GameObject.Find("Point").GetComponent<LineManager>();
        lineRenderer = GetComponent<LineRenderer>();

        // lineRenderer.useWorldSpace = false;

        mainCamera = Camera.main;

        //このラインオブジェクトを、位置はカメラ１０ｍ、回転はカメラと同じようになるようキープさせる
        transform.position = mainCamera.transform.position + mainCamera.transform.forward;
        transform.rotation = mainCamera.transform.rotation;

        // 座標指定の設定をローカル座標系にしたため、与える座標にも手を加える
        pos = Input.mousePosition;
        pos.z = 1f;

        // マウススクリーン座標をワールド座標に直す
        pos = mainCamera.ScreenToWorldPoint(pos);

        //// さらにそれをローカル座標に直す。
        pos = transform.InverseTransformPoint(pos);
        Debug.Log(pos);
        lineRenderer.SetPosition(0, pos);

        lineRenderer.SetPosition(1, pos);
    }


    /// <summary>
    ///
    /// </summary>
    private void LineContraction()
    {
        if (Input.GetMouseButton(0) && isTouch)
        {
            pos = Input.mousePosition;
            pos = mainCamera.ScreenToWorldPoint(pos);
            //// さらにそれをローカル座標に直す。
            pos = transform.InverseTransformPoint(pos);
           
            Debug.Log(lineRenderer);
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit2d = Physics2D.Raycast(ray.origin, ray.direction, maxDistance);
            //nameではなくinterfaceを使用する
            if (hit2d.collider.gameObject.tag == "Line")
            {
                Debug.Log(hit2d.collider.gameObject.name);
                IsCollision = true;
                currentPos.position = ray.origin;

                pos.z = 0;
                lineRenderer.SetPosition(0, pos);
            }
            else
                return;
            if (IsCollision)
            {
                //このラインオブジェクトを、位置はカメラ１０ｍ、回転はカメラと同じようになるようキープさせる
                transform.position = mainCamera.transform.position + mainCamera.transform.forward ;
                transform.rotation = mainCamera.transform.rotation;
                // 座標指定の設定をローカル座標系にしたため、与える座標にも手を加える
            }
        }

        if (Input.GetMouseButtonUp(0) && isTouch)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit2d = Physics2D.Raycast(ray.origin, ray.direction, maxDistance);
            //endPos.position = ray.origin;
            if (hit2d.collider.gameObject.layer == 7 || hit2d.collider.gameObject.layer == 8)
            {
                Debug.Log("aa");

                pos = Input.mousePosition;
                pos.z = 0.2f;

                // マウススクリーン座標をワールド座標に直す
                pos = mainCamera.ScreenToWorldPoint(pos);

                //// さらにそれをローカル座標に直す。
                pos = transform.InverseTransformPoint(pos);

               // lineRenderer.SetPosition(1, pos);
                isTouch = false;
                IsCollision = false;
                pos.z = 0;
                lineRenderer.SetPosition(1, pos);
                Debug.Log(lineRenderer.transform.position);
            }
            else if (hit2d.collider.gameObject.layer == 9)
            {
                lineManager.createLineCount++;
                Destroy(this.gameObject);
            }

        }
    }
}

