using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickPoint : MonoBehaviour
{
    public GameObject prfab;
    //�}�E�X�̈ʒu�����擾
    private Vector3 mousepPosition;

   

    // Update is called once per frame
    void Update()
    {

        //�}�E�X����������I�u�W�F�N�g����
        if(Input.GetMouseButtonDown(0))
        {
            mousepPosition = Input.mousePosition;
            mousepPosition.z = 10.0f;
            Instantiate(prfab,Camera.main.ScreenToWorldPoint(mousepPosition),Quaternion.identity);
        }
    }
}
