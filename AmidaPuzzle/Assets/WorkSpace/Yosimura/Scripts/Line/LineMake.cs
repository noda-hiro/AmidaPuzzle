using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineMake : MonoBehaviour
{
    private LineRenderer lineRenderer;

    private Camera mainCamera;

    private bool isTouch = true;

    private Vector3 pos;


    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.useWorldSpace = false;

        mainCamera = Camera.main;

        //���̃��C���I�u�W�F�N�g���A�ʒu�̓J�����P�O���A��]�̓J�����Ɠ����悤�ɂȂ�悤�L�[�v������
        transform.position = mainCamera.transform.position + mainCamera.transform.forward * 10;
        transform.rotation = mainCamera.transform.rotation;

        // ���W�w��̐ݒ�����[�J�����W�n�ɂ������߁A�^������W�ɂ����������
        pos = Input.mousePosition;
        pos.z = 10.0f;

        // �}�E�X�X�N���[�����W�����[���h���W�ɒ���
        pos = mainCamera.ScreenToWorldPoint(pos);

        // ����ɂ�������[�J�����W�ɒ����B
        pos = transform.InverseTransformPoint(pos);

        lineRenderer.SetPosition(0, pos);

        lineRenderer.SetPosition(1, pos);


    }

    // Update is called once per frame
    void Update()
    {




        // Debug.Log("ENTER");
        if (isTouch)
        {

            if (Input.GetMouseButton(0))
            {


                //���̃��C���I�u�W�F�N�g���A�ʒu�̓J�����P�O���A��]�̓J�����Ɠ����悤�ɂȂ�悤�L�[�v������
                transform.position = mainCamera.transform.position + mainCamera.transform.forward * 10;
                transform.rotation = mainCamera.transform.rotation;

                // ���W�w��̐ݒ�����[�J�����W�n�ɂ������߁A�^������W�ɂ����������
                pos = Input.mousePosition;
                pos.z = 10.0f;

                // �}�E�X�X�N���[�����W�����[���h���W�ɒ���
                pos = mainCamera.ScreenToWorldPoint(pos);

                // ����ɂ�������[�J�����W�ɒ����B
                pos = transform.InverseTransformPoint(pos);

                lineRenderer.SetPosition(1, pos);
            }

            if (Input.GetMouseButtonUp(0))
            {

                isTouch = false;

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//RAY�𐶐�
                RaycastHit2D hit = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction); // Ray�𓊎�


                 if (hit.collider == null)

                    {

                         //������Ȃ������Ƃ��̏���
                         Destroy(this.gameObject);
                    }
             }


     
        }

    }
        
        


}