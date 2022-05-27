using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateLine : MonoBehaviour
{
    private Vector3 m_mouseDownPosition = Vector3.zero;

    void OnMouseDown()
    {
        // マウスクリックした際の初期位置を保存。
        m_mouseDownPosition = transform.position;
    }

    void OnMouseDrag()
    {
        // マウスクリックした場所をワールド座標に変化して、
        // 初期位置とマウスクリック位置の中間にオブジェクトを配置。
        // オブジェクトのスケールを初期位置とマウスクリックの距離に。
        // オブジェクトの向きをマウスクリックした位置に。

        Vector3 inputPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 9.5f);
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(inputPosition);
        Vector3 mediumPos = (mousePos - m_mouseDownPosition) / 2.0f;
        float dist = Vector3.Distance(mousePos, m_mouseDownPosition);

        transform.position = mediumPos;
        transform.localScale = new Vector3(1.0f, 1.0f, dist);
        transform.LookAt(mousePos);
        Debug.Log("aa");
    }

    void OnMouseUp()
    {
        // 位置、回転、スケールを元に戻す。
        transform.position = m_mouseDownPosition;
        transform.rotation = Quaternion.identity;
        transform.localScale = Vector3.one;
    }
}
