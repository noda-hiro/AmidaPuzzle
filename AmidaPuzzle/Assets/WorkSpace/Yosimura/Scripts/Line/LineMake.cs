using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineMake : MonoBehaviour
{
    private LineRenderer lineRenderer;

    private Camera mainCamera;

    private bool isTouch = true;

    private Vector3 pos;


    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.useWorldSpace = false;

        mainCamera = Camera.main;

        //このラインオブジェクトを、位置はカメラ１０ｍ、回転はカメラと同じようになるようキープさせる
        transform.position = mainCamera.transform.position + mainCamera.transform.forward * 10;
        transform.rotation = mainCamera.transform.rotation;

        // 座標指定の設定をローカル座標系にしたため、与える座標にも手を加える
        pos = Input.mousePosition;
        pos.z = 10.0f;

        // マウススクリーン座標をワールド座標に直す
        pos = mainCamera.ScreenToWorldPoint(pos);

        // さらにそれをローカル座標に直す。
        pos = transform.InverseTransformPoint(pos);

        lineRenderer.SetPosition(0, pos);

        lineRenderer.SetPosition(1, pos);


    }

    // Update is called once per frame
    void Update()
    {




        // Debug.Log("ENTER");
        if (isTouch)
        {

            if (Input.GetMouseButton(0))
            {


                //このラインオブジェクトを、位置はカメラ１０ｍ、回転はカメラと同じようになるようキープさせる
                transform.position = mainCamera.transform.position + mainCamera.transform.forward * 10;
                transform.rotation = mainCamera.transform.rotation;

                // 座標指定の設定をローカル座標系にしたため、与える座標にも手を加える
                pos = Input.mousePosition;
                pos.z = 10.0f;

                // マウススクリーン座標をワールド座標に直す
                pos = mainCamera.ScreenToWorldPoint(pos);

                // さらにそれをローカル座標に直す。
                pos = transform.InverseTransformPoint(pos);

                lineRenderer.SetPosition(1, pos);
            }

            if (Input.GetMouseButtonUp(0))
            {

                isTouch = false;

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//RAYを生成
                RaycastHit2D hit = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction); // Rayを投射


                 if (hit.collider == null)

                    {

                         //当たらなかったときの処理
                         Destroy(this.gameObject);
                    }
             }


     
        }

    }
        
        


}