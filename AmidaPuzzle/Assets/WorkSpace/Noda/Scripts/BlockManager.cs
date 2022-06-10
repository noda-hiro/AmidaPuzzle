using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockManager : MonoBehaviour
{
    private Button startBtn;
    [SerializeField] private BlockContorller oneLineblockContorller; 
    [SerializeField] private BlockContorller twoLineblockContorller;
    [SerializeField] private BlockContorller block;
    [SerializeField] private BlockContorller block2;
    [SerializeField] private GameObject clearPanel;

    public void OnStartBtn()
    {
        oneLineblockContorller.enabled = true;
        twoLineblockContorller.enabled = true;
        startBtn.interactable = false;
    }

    private void Start()
    {
        startBtn = GameObject.Find("StartButton").GetComponent<Button>();
        startBtn.onClick.AddListener(OnStartBtn);
    }

    private void Update()
    {
        if(block.IsGool&&block2.IsGool)
        {
            clearPanel.SetActive(true);
        }
    }

}
