using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateLines : SingletonMonobehaviour<CreateLines>
{
    public VerticalLine verticalLine;
    public int NumberOfUnitsRequired = 0;
    public LineRenderer lineList;
    public GameObject linePrefab;
    public GameObject line;
    public List<GameObject> linePrefabs = new List<GameObject>();
    public List<GameObject> lineCreated = new List<GameObject>();
    [SerializeField] private Transform parentObj;
    [SerializeField] private int maxCount = 0;
    public int currentCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        //for (int i = 0; i < linePrefabs.Count; i++)
        //{
        //    var obj = Instantiate(linePrefab, new Vector2(1500, 0), Quaternion.identity, parentObj.parent);
        //    verticalLine = obj.GetComponent<VerticalLine>();
        //    linePrefabs[i] = obj;
        //    verticalLine.verticalLineNum = i + 1;
        //    lineList = obj.gameObject.GetComponent<LineRenderer>();
        //    lineList.SetPosition(0, new Vector2(1500f, 0f));
        //    lineList.SetPosition(1, new Vector2(1500f, 0f));
        //}
    }


    public void Createline()
    {
        if (line == null && currentCount < maxCount)
        {
            line = Instantiate(linePrefab, Vector2.zero, Quaternion.identity, parentObj.parent);
        }

        if (currentCount == maxCount)
        {
            this.enabled = false;
        }
    }
}
