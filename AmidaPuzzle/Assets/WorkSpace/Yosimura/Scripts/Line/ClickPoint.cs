using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickPoint : MonoBehaviour
{
    public GameObject prfab;
    //マウスの位置情報を取得
    private Vector3 mousepPosition;

   

    // Update is called once per frame
    void Update()
    {

        //マウスを押したらオブジェクト生成
        if(Input.GetMouseButtonDown(0))
        {
            mousepPosition = Input.mousePosition;
            mousepPosition.z = 10.0f;
            Instantiate(prfab,Camera.main.ScreenToWorldPoint(mousepPosition),Quaternion.identity);
        }
    }
}
