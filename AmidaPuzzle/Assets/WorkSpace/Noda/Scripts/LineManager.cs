using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineManager : MonoBehaviour
{
    [SerializeField]
    private GameObject lineObj;
    [SerializeField]
    private GameObject parent;
    public int createLineCount = 2;
    private int LimitCreateCount = 0;
    [SerializeField] private Text RemainingLineCountText;
    private float maxDistance = 10f;

    //現在のマウス位置
    Vector3 currentPos;

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        //currentPos = Input.mousePosition;
        //currentPos.z = 20;
        currentPos.z = 0;
        //currentPos = mainCamera.ScreenToWorldPoint(currentPos);

        //currentPos = transform.InverseTransformPoint(currentPos);
        //クリックしたらactionを作動
        if (Input.GetMouseButtonDown(0) && createLineCount > LimitCreateCount)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit2d = Physics2D.Raycast(ray.origin, ray.direction, maxDistance);
            if (hit2d.collider.gameObject.name == "FirstLine" || hit2d.collider.gameObject.name == "SecondLine")
            {
                createLineCount--;
                InstLineObj();
            }
        }
        RemainingLineCountText.text = "引く線の数 " + createLineCount;
    }

    void InstLineObj()
    {
        //Instantiate(lineObj, currentPos, Quaternion.identity, parent.transform.parent);
        GameObject game = Instantiate(lineObj, new Vector3(transform.position.x,transform.position.y,parent.transform.position.z), Quaternion.identity);
        game.transform.SetParent(parent.transform.parent,true);
    }


}
