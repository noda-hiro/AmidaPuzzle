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
            if (Physics.Raycast(ray, out hit)) // Ray�𓊎�
            {
                if (hit.collider.CompareTag("Line")) // �^�O���r
                {
                    Debug.Log("Hit"); // �I�u�W�F�N�g�ɓ���������HIT�̃��O�𗬂�
                }
            }
        }
    }
}
    
 

