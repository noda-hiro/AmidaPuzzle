using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using System;

public enum BlockColorType
{
    RED = 1,
    ORANGE = 2,
    YELLOW = 3,
    GREEN = 4,
    RIGHTBLUE = 5,
    BLUE = 6,
    PURPLE = 7,
}
public enum BlockType
{
    U_ver1 = 1,
    U_ver2 = 2,
    L_ver1 = 3,
    L_ver2 = 4,
    I_ver1 = 5,
    I_ver2 = 6,
    E_ver1 = 7,
    E_ver2 = 8,
    O_ver1 = 9,
    O_ver2 = 10,
    T_ver1 = 11,
    T_ver2 = 12,
}

public class BLOCK : MonoBehaviour
{
    [SerializeField] private Button btn;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private PointClass point;
    private int collisionCount = 1;
    [SerializeField] private int myBlockLineNum = -1;
    private bool isHit = false;
    private Vector3 currentPos;
    private int nextNumber = -1;
    private bool isBlockHit = false;
    public bool isSwitching = false;
    public BlockColorType blockColorType = BlockColorType.RED;
    public BlockType blockType = BlockType.U_ver1;
    public int puzzleCount = 0;
    private BlockSpriteChange blockSpriteChange = null;
    public bool isComplete = false;
    public int finishNum;
    [SerializeField]
    private bool onTheLine;
    [SerializeField] private EndPosController posController;

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
            if (blockColorType == BType.blockColorType)
            {
                Debug.Log("������");
                if (puzzleCount < BType.puzzleCount)
                {
                    posController.blockList.Remove(this);
                    Destroy(this.gameObject);
                }
                else
                {
                    BlockCoalescingCalculations(BType.puzzleCount);
                    Debug.Log("aifa");
                }
            }
            else if(blockColorType != BType.blockColorType&&onTheLine==false)
            {
                isSwitching = !isSwitching;
                isInversion = true;

                StartCoroutine(MoveToDestinationPoint(pointClass._currentPoint, 0, isSwitching));
                Debug.Log(pointClass._currentPoint);
            }
        }
        //�Ԃ������̂��|�C���g�Ńu���b�N�̃^�C�v���t�����Ȃ� ��
        else if (collisionCount == -1 && onTheLine == false)
        {
            StartCoroutine(MoveToDestinationPoint(collsionPoint._twoPoint, 0, isSwitching));
            Debug.Log("���]������");
        }
        //�Ԃ������̂��S�[���Ȃ�@�S�[��
        else if (collision.gameObject.layer == 11)
        {
            //todo�@�R���[�`���~�܂�Ȃ������炲�߂�
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            //isComplete = true;
        }
        //�Ԃ������̂��|�C���g�Ȃ�@�E����
        else if (collision.gameObject.layer == 8)
        {
            pointClass = collision.GetComponent<PointClass>();
            //���݂̋t 0or1 �r�b�g���Z�ɋ߂�����
            isSwitching = !isSwitching;
            var nextPos = collision.gameObject.GetComponent<PointClass>();
            hitNum = nextPos.PointNumber;
            if (hitNum % 2 == 0)
            {
                if (hitNum == nextNumber || isInversion)
                {
                    StartCoroutine(MoveToDestinationPoint(nextPos._twoPoint, 0, isSwitching));
                    isInversion = false;
                    onTheLine = true;
                    Debug.Log("1"+nextPos._twoPoint);
                }
                else
                {
                    nextNumber = nextPos.PointNumber + 1;
                    StartCoroutine(MoveToDestinationPoint(nextPos._onePoint, 0, isSwitching));
                    onTheLine = false;
                    Debug.Log("2"+nextPos._onePoint);
                }
            }
            else if (hitNum % 2 != 0)
            {
                if (nextPos.PointNumber == nextNumber || isInversion)
                {
                    StartCoroutine(MoveToDestinationPoint(nextPos._twoPoint, 0, isSwitching));
                    isInversion = false;
                    onTheLine = true;
                    Debug.Log("3"+nextPos._twoPoint);
                }
                else
                {
                    nextNumber = nextPos.PointNumber - 1;
                    StartCoroutine(MoveToDestinationPoint(nextPos._onePoint, 0, isSwitching));
                    onTheLine = false;
                    Debug.Log("4"+nextPos._onePoint);
                }
            }
        }
    }

    private void BlockCoalescingCalculations(int collisioPuzzleCount)
    {
        var block = (int)blockType;

        if (block == 1 && collisioPuzzleCount == 2
            || block == 2 && collisioPuzzleCount == 1)
        {
            this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            this.gameObject.transform.GetChild(1).gameObject.SetActive(true);
            blockSpriteChange.ChangeBlockSprite(this.gameObject, 1);
            puzzleCount = 10;
        }
        else if (block == 2 && collisioPuzzleCount == 3
            || block == 3 && collisioPuzzleCount == 2)
        {
            this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            this.gameObject.transform.GetChild(1).gameObject.SetActive(true);
            blockSpriteChange.ChangeBlockSprite(this.gameObject, 2);
            puzzleCount = 30;
        }
        else if (block == 5 && collisioPuzzleCount == 6
            || block == 6 && collisioPuzzleCount == 5)
        {
            this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            this.gameObject.transform.GetChild(1).gameObject.SetActive(true);
            blockSpriteChange.ChangeBlockSprite(this.gameObject, 3);
            puzzleCount = 30;
        }
        else if (block == 7 && collisioPuzzleCount == 8
           || block == 8 && collisioPuzzleCount == 7)
        {
            this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            this.gameObject.transform.GetChild(1).gameObject.SetActive(true);
            blockSpriteChange.ChangeBlockSprite(this.gameObject, 6);
            puzzleCount = 60;
        }

    }

}