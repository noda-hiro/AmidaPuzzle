using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Display : MonoBehaviour
{
    public GameObject textGameObject;
    public Button button;

    void Start()
    {
        bool isActive = false; 

       button.onClick.AddListener(() =>
        {
            isActive = !isActive;
            textGameObject.SetActive(isActive);
        });
       
    }

    void Update()
    {
        bool isActive = false;
        button.onClick.AddListener(() =>
        {
            if (isActive == true)
            {
                isActive = false;
            }

            else if(isActive == false)
            {
                isActive = true;
            }
        });
    }
}
