//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//enum ColorType
//{
//    red,
//    blue,
//    yellow,
//}
//enum BlockType
//{
//    NORMAL,
//    INVERSE_FACING,
//    UNDR,
//    FINISH,
//}


//public class BlockContorller : MonoBehaviour, IChangeVector
//{
//    public int blockNumber = 1;
//    private ColorType colorType = ColorType.red;
//    private BlockType blockType = BlockType.NORMAL;
//    private Transform FirstPos;
//    private Transform EndPos;
//    private readonly float moveSpeed = 0.02f;
//    public Action OnCollisionBlockListener;
//    private bool IsCollsion = false;
//    private int collisionCount = 1;
//    [SerializeField] private PointClass point;
//    public bool IsGool = false;


//    // Start is called before the first frame update
//    void Start()
//    {
//        DestinationPointSetting();
//        StartCoroutine(MoveToDestinationPoint(EndPos, 1));
//    }

//    /// <summary>
//    /// �ړ�����
//    /// </summary>
//    /// <param name="nextPos">���̈ړ��n�_</param>
//    /// <returns></returns>
//    public IEnumerator MoveToDestinationPoint(Transform nextPos, int count)
//    {
//        //   Debug.Log(nextPos.transform);
//        var dis = Vector3.Distance(transform.position, nextPos.transform.position);
//        while (true)
//        {
//            transform.position = Vector3.MoveTowards(this.transform.position, nextPos.transform.position, moveSpeed);
//            //transform.position = Vector3.MoveTowards(this.transform.position, nextPos.transform.position, moveSpeed);
//            yield return new WaitForSeconds(0.01f);

//            if (IsCollsion)
//            {
//                IsCollsion = false;
//                collisionCount = count;
//                yield break;
//            }
//        }
//    }


//    /// <summary>
//    /// ��������ɓ����������̔���
//    /// </summary>
//    /// <param name="collision">�Փ˂����I�u�W�F�N�g</param>
//    private void OnTriggerEnter2D(Collider2D collision)
//    {
//        IsCollsion = true;

//        var collsionPoint = collision.gameObject.GetComponent<PointClass>();
//        //�Ԃ������̂��u���b�N��������
//        if (collision.name == "Block")
//        {
//            StartCoroutine(MoveToDestinationPoint(point._currentPoint, 1));
//        }
//        //�Ԃ������̂��|�C���g�Ńu���b�N�̃^�C�v���t�����Ȃ�
//        else if (collisionCount == -1)
//        {
//            StartCoroutine(MoveToDestinationPoint(collsionPoint._twoPoint.transform, 1));
//        }
//        //�Ԃ������̂��S�[���Ȃ�
//        else if (collision.gameObject.tag == "Finish")
//        {
//            this.gameObject.GetComponent<Collider2D>().enabled = false;
//            IsGool = true;
//        }
//        //�Ԃ������̂��|�C���g�Ȃ�
//        else if (collisionCount == 1)
//        {
//            point = collsionPoint;
//            StartCoroutine(MoveToDestinationPoint(collsionPoint._onePoint.transform, -1));
//        }

//    }

//    /// <summary>
//    /// �e�u���b�N�̏I���_������
//    /// </summary>
//    private void DestinationPointSetting()
//    {
//        if (blockNumber == 1)
//            EndPos = GameObject.Find("Firstline_EndPos").gameObject.GetComponent<Transform>();
//        else if (blockNumber == 2)
//            EndPos = GameObject.Find("Secondline_EndPos").gameObject.GetComponent<Transform>();
//    }

//    public void ChangeVector()
//    {
//        Debug.Log("faf");
//    }
//}
