using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using System;

public enum BlockType
{
    RED = 1,
    ORANGE = 2,
    YELLOW = 3,
    GREEN = 4,
    RIGHTBLUE = 5,
    BLUE = 6,
    PURPLE = 7,
}



public class BLOCK : MonoBehaviour
{
    [SerializeField] private Button btn;
    private float moveSpeed = 0.5f;
    [SerializeField] private PointClass point;
    private int collisionCount = 1;
    [SerializeField] private int myBlockLineNum = -1;
    private bool isHit = false;
    private Vector3 currentPos;
    private int nextNumber = -1;
    private bool isBlockHit = false;
    public bool isSwitching = false;
    public BlockType blockType = BlockType.RED;
    [SerializeField]
    private int puzzleCount = 0;
    private BlockSpriteChange blockSpriteChange = null;

    private void Start()
    {
        blockSpriteChange = GameObject.FindWithTag("BlockSprite").GetComponent<BlockSpriteChange>();
    }

    public IEnumerator MoveToDestinationPoint(Vector3 nextPos, int count, bool istemp)
    {
        while (true)
        {
            if (this.transform.position == nextPos)
            {
                yield break;
            }
            if (istemp != isSwitching)
            {
                yield break;
            }
            transform.position = Vector2.MoveTowards(this.transform.position, nextPos, moveSpeed);
            yield return new WaitForSeconds(0.001f);
        }
    }

    private int hitNum = 0;
    PointClass pointClass = null;
    private bool isInversion = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var collsionPoint = collision.gameObject.GetComponent<PointClass>();

        if (collision.tag == "Block")
        {
            var BType = collision.gameObject.GetComponent<BLOCK>();
            Debug.Log(BType.blockType);


            if (blockType == BType.blockType)
            {
                if (puzzleCount < BType.puzzleCount)
                    Destroy(this.gameObject);
                else
                    blockSpriteChange.ChangeBlockSprite(this.gameObject, 4);
            }
            else
            {
                isSwitching = !isSwitching;
                isInversion = true;
                StartCoroutine(MoveToDestinationPoint(pointClass._currentPoint, 0, isSwitching));
            }
        }
        //ぶつかったのがポイントでブロックのタイプが逆向きなら ↓
        else if (collisionCount == -1)
        {
            StartCoroutine(MoveToDestinationPoint(collsionPoint._twoPoint, 0, isSwitching));
        }
        //ぶつかったのがゴールなら　ゴール
        else if (collision.gameObject.layer == 11)
        {
            //todo　コルーチン止まらなかったらごめん
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
        //ぶつかったのがポイントなら　右か左
        else if (collision.gameObject.layer == 8)
        {
            pointClass = collision.GetComponent<PointClass>();
            //現在の逆 0or1 ビット演算に近いもの
            isSwitching = !isSwitching;
            var nextPos = collision.gameObject.GetComponent<PointClass>();
            hitNum = nextPos.PointNumber;
            if (hitNum % 2 == 0)
            {
                if (hitNum == nextNumber || isInversion)
                {
                    StartCoroutine(MoveToDestinationPoint(nextPos._twoPoint, 0, isSwitching));
                    isInversion = false;
                }
                else
                {
                    nextNumber = nextPos.PointNumber + 1;
                    StartCoroutine(MoveToDestinationPoint(nextPos._onePoint, 0, isSwitching));
                }
            }
            else if (hitNum % 2 != 0)
            {
                if (nextPos.PointNumber == nextNumber || isInversion)
                {
                    StartCoroutine(MoveToDestinationPoint(nextPos._twoPoint, 0, isSwitching));
                    isInversion = false;
                }
                else
                {
                    nextNumber = nextPos.PointNumber - 1;
                    StartCoroutine(MoveToDestinationPoint(nextPos._onePoint, 0, isSwitching));
                }
            }
        }
    }


}