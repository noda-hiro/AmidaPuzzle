using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineMg : MonoBehaviour
{

    private Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) // Rayを投射
            {
                if (hit.collider.CompareTag("Line")) // タグを比較
                {
                    Debug.Log("Hit"); // オブジェクトに当たったらHITのログを流す
                }
            }
        }
    }
}
    
 

