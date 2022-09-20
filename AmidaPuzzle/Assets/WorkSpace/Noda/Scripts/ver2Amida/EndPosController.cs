using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UniRx;
using UniRx.Triggers;

public class EndPosController : MonoBehaviour
{
    public List<End> isClearList = new List<End>();
    public GameObject clearPanel = null;
    public GameObject failurePanel = null;
    [SerializeField] private List<Transform> prefabPos = new List<Transform>();
    [SerializeField] private GameObject effectPrefab;
    public List<BLOCK> blockList = new List<BLOCK>();
    private int count = 0;
    public int nowClearBlockCount = -1;
    public int clearPointCount = 2;
    public int currentNum = 0;
    [SerializeField] public CreateLines createLines;
    WaitForSeconds waitTime = new WaitForSeconds(1f);


    private void Start()
    {
        this.UpdateAsObservable().Where(_ => Input.GetKeyDown(KeyCode.Space)).Subscribe(_ =>
        {
           StartCoroutine(CheckClear());
        });
    }
    private void Update()
    {
        //CheckClear();
    }

    public IEnumerator CheckClear()
    {
        if (nowClearBlockCount == 1 || nowClearBlockCount == 2 || nowClearBlockCount == 3 || nowClearBlockCount == 4)
        {
            if (isClearList.All(i => i.isClear == true))
            {
                Debug.Log(effectPrefab);
                effectPrefab.gameObject.SetActive(true);
                yield return waitTime;
                clearPanel.SetActive(true);
                failurePanel.SetActive(false);
                yield break;
            }
            //else if (isClearList.All(i => i.isClear == false))
            //    failurePanel.SetActive(true);
            else
                failurePanel.SetActive(true);
        }
    }

}
