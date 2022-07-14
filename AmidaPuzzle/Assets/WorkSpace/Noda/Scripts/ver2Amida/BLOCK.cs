using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using System;

public class BLOCK : MonoBehaviour
{
    [SerializeField] private Button btn;
    private float moveSpeed = 1f;
    [SerializeField] private PointClass point;
    private int collisionCount = 1;
    [SerializeField] private int myBlockLineNum = -1;
    private bool isHit = false;
    private Vector3 currentPos;
    private int nextNumber = -1;
    private bool isBlockHit = false;
    public bool isSwitching = false;

    private void Start()
    {
        btn.onClick.AddListener(() => OnPart());
        //    EnterTest();
    }

    private void OnPart()
    {
        // StartCoroutine(MoveToDestinationPoint(endPosList[myBlockLineNum].position, 0, isSwitching));
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


    //private void EnterTest()
    //{
    //    this.OnTriggerEnter2DAsObservable().ThrottleFirst(TimeSpan.FromSeconds(0.01f)).Subscribe(x =>
    //    {

    //        var collsionPoint = x.gameObject.GetComponent<PointClass>();
    //        //�u���b�N�Ȃ�@�~�܂�
    //        if (x.name == "Block")
    //        {
    //            Debug.Log("aaaa");
    //            isSwitching = !isSwitching;
    //            StartCoroutine(MoveToDestinationPoint(collsionPoint._currentPoint, 0, isSwitching));
    //        }
    //        //�Ԃ������̂��|�C���g�Ńu���b�N�̃^�C�v���t�����Ȃ� ��
    //        else if (collisionCount == -1)
    //        {
    //            StartCoroutine(MoveToDestinationPoint(collsionPoint._twoPoint, 0, isSwitching));
    //        }
    //        //�Ԃ������̂��S�[���Ȃ�@�S�[��
    //        else if (x.gameObject.layer == 11)
    //        {
    //            //todo�@�R���[�`���~�܂�Ȃ������炲�߂�
    //            this.gameObject.GetComponent<BLOCK>().enabled = false;
    //        }
    //        //�Ԃ������̂��|�C���g�Ȃ�@�E����
    //        else if (x.gameObject.layer == 8)
    //        {
    //            //���݂̋t 0or1 �r�b�g���Z�ɋ߂�����
    //            isSwitching = !isSwitching;
    //            var nextPos = x.gameObject.GetComponent<PointClass>();
    //            hitNum = nextPos.PointNumber;
    //            if (hitNum % 2 == 0)
    //            {
    //                if (nextPos.PointNumber == nextNumber)
    //                {
    //                    StartCoroutine(MoveToDestinationPoint(nextPos._twoPoint, 0, isSwitching));
    //                }
    //                else
    //                {
    //                    nextNumber = nextPos.PointNumber + 1;
    //                    StartCoroutine(MoveToDestinationPoint(nextPos._onePoint, 0, isSwitching));
    //                }
    //            }
    //            else if (nextPos.PointNumber % 2 != 0)
    //            {
    //                if (nextPos.PointNumber == nextNumber)
    //                    StartCoroutine(MoveToDestinationPoint(nextPos._twoPoint, 0, isSwitching));
    //                else
    //                {
    //                    nextNumber = nextPos.PointNumber - 1;
    //                    StartCoroutine(MoveToDestinationPoint(nextPos._onePoint, 0, isSwitching));
    //                }
    //            }
    //        }
    //    });
    //}

    private int hitNum = 0;
    PointClass pointClass = null;
    private bool isInversion = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var collsionPoint = collision.gameObject.GetComponent<PointClass>();
        //�u���b�N�Ȃ�@�~�܂�
        if (collision.tag == "Block")
        {
            Debug.LogError("aa");
            isSwitching = !isSwitching;
            isInversion = true;
            StartCoroutine(MoveToDestinationPoint(pointClass._currentPoint, 0, isSwitching));
        }
        //�Ԃ������̂��|�C���g�Ńu���b�N�̃^�C�v���t�����Ȃ� ��
        else if (collisionCount == -1)
        {
            StartCoroutine(MoveToDestinationPoint(collsionPoint._twoPoint, 0, isSwitching));
        }
        //�Ԃ������̂��S�[���Ȃ�@�S�[��
        else if (collision.gameObject.layer == 11)
        {
            //todo�@�R���[�`���~�܂�Ȃ������炲�߂�
            this.gameObject.GetComponent<BLOCK>().enabled = false;
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