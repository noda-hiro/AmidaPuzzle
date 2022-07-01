using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineMg : MonoBehaviour
{

    public GameObject prefab;
    private Vector3 mousePosition;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//RAY�𐶐�
            RaycastHit2D hit = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction); // Ray�𓊎�



           // Debug.Log("aaa");

                if (hit.collider.CompareTag("Line")) // �^�O���r
                {
                //  Debug.Log("hit");
                //����������Pefab�I�u�W�F�N�g���w�肵,�}�E�X�̍��W�ɐ���
                mousePosition = Input.mousePosition;
                mousePosition.z = 10.0f;
                Instantiate(prefab, Camera.main.ScreenToWorldPoint(mousePosition), Quaternion.identity);

                 }
            
        }
    }
}
    
 

