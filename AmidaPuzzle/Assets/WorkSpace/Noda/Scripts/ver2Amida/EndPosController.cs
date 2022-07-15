using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UniRx;
using UniRx.Triggers;

public class EndPosController : MonoBehaviour
{
    [SerializeField] private List<End> isClearList = new List<End>();
    [SerializeField] private GameObject clearPanel = null;
    [SerializeField] private GameObject failurePanel = null;
    [SerializeField] private List<Transform> prefabPos = new List<Transform>();
    private int count = 0;
    public int nowClearBlockCount = 0;

    private void Start()
    {
        this.UpdateAsObservable().Where(_ => Input.GetKeyDown(KeyCode.Space)).Subscribe(_ =>
        {
            CheckClear();
        });
    }
    private void Update()
    {
        CheckClear();
    }

    public void CheckClear()
    {
        if (nowClearBlockCount == 1 || nowClearBlockCount == 2 || nowClearBlockCount == 3 || nowClearBlockCount == 4)
        {
            if (isClearList.All(i => i.isClear == true))
            {
                clearPanel.SetActive(true);
                failurePanel.SetActive(false);
            }
            //else if (isClearList.All(i => i.isClear == false))
            //    failurePanel.SetActive(true);
            else
                failurePanel.SetActive(true);
        }
    }
}
