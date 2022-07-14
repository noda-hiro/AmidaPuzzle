using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] public int maxCount = 0;
    public int currentCount = 0;
    public int lineCount = 0;
    [SerializeField] private LinesPresenter linesPresenter;
    [SerializeField] private Text lineCountText;
    // Start is called before the first frame update
    void Start()
    {
        lineCount = maxCount;
        lineCountText.text = "引く線の数：" + lineCount + "本";
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

    public void UpdateLineCountText()
    {
        lineCount--;
        lineCountText.text = "引く線の数：" + lineCount + "本";
    }

}
