using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System.Linq;

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
    [SerializeField] public List<GameObject> prefabList = new List<GameObject>();
    [SerializeField] private EndPosController posController = null;
    private int count;

    private void Start()
    {
        nowClearBlockCount = 0;
        count = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var block = collision.gameObject.GetComponent<BLOCK>();

        block.isComplete = true;

        if (block.puzzleCount == (int)posType
              && (int)endPosBlockColor == (int)block.blockColorType)
        {
            isClear = true;
        }

        if (posController.blockList.All(i => i.isComplete == true))
        {
            if (posController.isClearList.All(i => i.isClear == true))
            {
                posController.createLines.ReMoveLine();
                posController.clearPanel.SetActive(true);
                return;
            }
            else if (posController.isClearList.All(i => i.isClear != true))
            {
                posController.createLines.ReMoveLine();
                posController.failurePanel.SetActive(true);
                return;
            }
            else if (posController.isClearList[0].isClear==true&& posController.isClearList[1].isClear == false|| posController.isClearList[0].isClear == false&& posController.isClearList[1].isClear == true)
            {
                posController.createLines.ReMoveLine();
                posController.failurePanel.SetActive(true);
            }
        }
    }
}

