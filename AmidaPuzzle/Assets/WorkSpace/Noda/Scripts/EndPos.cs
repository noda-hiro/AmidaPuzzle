using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPos : MonoBehaviour
{
    [SerializeField] private int endPosNumber = 0;
    public int goolCount = 0;
    [SerializeField] GameObject clearResultScreen;
    [SerializeField] GameObject failureResultScreen;
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var blockObj = collision.gameObject.GetComponent<BlockContorller>().blockNumber;
        if (blockObj == endPosNumber)
            goolCount++;
        else
            goolCount--;
    }
}
