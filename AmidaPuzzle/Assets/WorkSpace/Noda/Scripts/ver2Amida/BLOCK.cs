using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BLOCK : MonoBehaviour
{

    private float moveSpeed = 0.02f;
    [SerializeField] private PointClass point;
    private int collisionCount = 1;

    public IEnumerator MoveToDestinationPoint(Transform nextPos, int count)
    {
        //   Debug.Log(nextPos.transform);
        var dis = Vector3.Distance(transform.position, nextPos.transform.position);
        while (true)
        {
            transform.position = Vector3.MoveTowards(this.transform.position, nextPos.transform.position, moveSpeed);
            yield return new WaitForSeconds(0.01f);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        var collsionPoint = collision.gameObject.GetComponent<PointClass>();

        if (collision.name == "Block")
        {
            StartCoroutine(MoveToDestinationPoint(point._currentPoint.transform, 1));
        }
        //ぶつかったのがポイントでブロックのタイプが逆向きなら
        else if (collisionCount == -1)
        {
            StartCoroutine(MoveToDestinationPoint(collsionPoint._twoPoint.transform, 1));
        }
        //ぶつかったのがゴールなら
        else if (collision.gameObject.tag == "Finish")
        {
            this.gameObject.GetComponent<Collider2D>().enabled = false;
        }
        //ぶつかったのがポイントなら
        else if (collisionCount == 1)
        {
            point = collsionPoint;
            StartCoroutine(MoveToDestinationPoint(collsionPoint._onePoint.transform, -1));
        }
    }
}
