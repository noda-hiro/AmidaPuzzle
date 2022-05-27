using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum ColorType
{
    red,
    blue,
    yellow,
}

public class BlockContorller : MonoBehaviour
{
    [SerializeField] private int blockNumber = 1;
    private ColorType colorType = ColorType.red;
    private Transform FirstPos;
    private Transform EndPos;
    private readonly float moveSpeed = 0.01f;
    public Action OnCollisionBlockListener;
    private bool IsCollsion = false;

    // Start is called before the first frame update
    void Start()
    {
        DestinationPointSetting();
        StartCoroutine(MoveToDestinationPoint(EndPos));
    }

    /// <summary>
    /// à⁄ìÆèàóù
    /// </summary>
    /// <param name="nextPos">éüÇÃà⁄ìÆínì_</param>
    /// <returns></returns>
    public IEnumerator MoveToDestinationPoint(Transform nextPos)
    {
        Debug.Log(nextPos.transform);
        var dis = Vector3.Distance(transform.position, nextPos.transform.position);
        while (true)
        {
            transform.position = Vector3.MoveTowards(this.transform.position, nextPos.transform.position, moveSpeed);
            yield return new WaitForSeconds(0.01f);

            if (IsCollsion)
            {
                IsCollsion = false;
                yield break;
            }
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        IsCollsion = true;

       // if (OnCollisionBlockListener != null)
        //{
          //  OnCollisionBlockListener();

            var collsionPoint = collision.gameObject.GetComponent<PointClass>();
            StartCoroutine(MoveToDestinationPoint(collsionPoint._onePoint.transform));
            Debug.Log(collsionPoint._onePoint);
        //}
    }

    private void DestinationPointSetting()
    {
        if (blockNumber == 1)
            EndPos = GameObject.Find("Firstline_EndPos").gameObject.GetComponent<Transform>();
        else if (blockNumber == 2)
            EndPos = GameObject.Find("Secondline_EndPos").gameObject.GetComponent<Transform>();
    }

}
