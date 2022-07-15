using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
public class End : MonoBehaviour
{
    enum EndPosType
    {
        T = 10,
        O = 20,
        E = 30,
        U = 40,
        L = 50,
        I = 60,
    }

    enum EndPosBlockColorType
    {
        RED = 1,
        ORANGE = 2,
        YELLOW = 3,
        GREEN = 4,
        RIGHTBLUE = 5,
        BLUE = 6,
        PURPLE = 7,
    }


    public bool isClear = false;
    [SerializeField]
    private EndPosType posType = EndPosType.T;
    [SerializeField]
    private EndPosBlockColorType endPosBlockColor = EndPosBlockColorType.BLUE;
    public int nowClearBlockCount { get; private set; }
    [SerializeField] private List<GameObject> prefabList = new List<GameObject>();
    [SerializeField] private EndPosController posController = null;

    private void Start()
    {
        nowClearBlockCount = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var block = collision.gameObject.GetComponent<BLOCK>();

        if (block.puzzleCount == (int)posType
            && (int)endPosBlockColor == (int)block.blockColorType)
        {
            isClear = true;
            nowClearBlockCount++;
            posController.nowClearBlockCount++;
        }
    }
}

