using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineMg : MonoBehaviour
{

    [SerializeField]
    private GameObject lineObj;

    //���݂̃}�E�X�ʒu
    Vector3 currentPos;

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;

    }

    private void Update()
    {
        currentPos = Input.mousePosition;
        currentPos.z = 20;

        currentPos = mainCamera.ScreenToWorldPoint(currentPos);

        currentPos = transform.InverseTransformPoint(currentPos);

        if (Input.GetMouseButtonDown(0))
        {


            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//RAY�𐶐�
            RaycastHit2D hit = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction); // Ray�𓊎�

            if (hit.collider.CompareTag("Line")) // �^�O���r
            {
                InstLineObj();

            }

        }

      
    }

    void InstLineObj()
    {
        Instantiate(lineObj, currentPos, Quaternion.identity);
    }
}
    
 

