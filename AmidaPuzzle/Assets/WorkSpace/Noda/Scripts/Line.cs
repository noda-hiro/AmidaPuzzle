using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private Camera mainCamera;
    private bool isTouch = true;
    private Vector3 pos;
    private float maxDistance = 1f;
    private bool IsCollision = false;
    private Transform currentPos;
    private Transform endPos;
    private LineManager lineManager;

    // Start is called before the first frame update
    void Start()
    {
        SettingLineRender();
    }

    // Update is called once per frame
    void Update()
    {
      LineContraction();
    }

    /// <summary>
    /// line�̏����ݒ�
    /// </summary>
    private void SettingLineRender()
    {
        lineManager = GameObject.Find("Point").GetComponent<LineManager>();
        lineRenderer = GetComponent<LineRenderer>();

        // lineRenderer.useWorldSpace = false;

        mainCamera = Camera.main;

        //���̃��C���I�u�W�F�N�g���A�ʒu�̓J�����P�O���A��]�̓J�����Ɠ����悤�ɂȂ�悤�L�[�v������
        transform.position = mainCamera.transform.position + mainCamera.transform.forward;
        transform.rotation = mainCamera.transform.rotation;

        // ���W�w��̐ݒ�����[�J�����W�n�ɂ������߁A�^������W�ɂ����������
        pos = Input.mousePosition;
        pos.z = 1f;

        // �}�E�X�X�N���[�����W�����[���h���W�ɒ���
        pos = mainCamera.ScreenToWorldPoint(pos);

        //// ����ɂ�������[�J�����W�ɒ����B
        pos = transform.InverseTransformPoint(pos);
        Debug.Log(pos);
        lineRenderer.SetPosition(0, pos);

        lineRenderer.SetPosition(1, pos);
    }


    /// <summary>
    ///
    /// </summary>
    private void LineContraction()
    {
        if (Input.GetMouseButton(0) && isTouch)
        {
            pos = Input.mousePosition;
            pos = mainCamera.ScreenToWorldPoint(pos);
            //// ����ɂ�������[�J�����W�ɒ����B
            pos = transform.InverseTransformPoint(pos);
           
            Debug.Log(lineRenderer);
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit2d = Physics2D.Raycast(ray.origin, ray.direction, maxDistance);
            //name�ł͂Ȃ�interface���g�p����
            if (hit2d.collider.gameObject.tag == "Line")
            {
                Debug.Log(hit2d.collider.gameObject.name);
                IsCollision = true;
                currentPos.position = ray.origin;

                pos.z = 0;
                lineRenderer.SetPosition(0, pos);
            }
            else
                return;
            if (IsCollision)
            {
                //���̃��C���I�u�W�F�N�g���A�ʒu�̓J�����P�O���A��]�̓J�����Ɠ����悤�ɂȂ�悤�L�[�v������
                transform.position = mainCamera.transform.position + mainCamera.transform.forward ;
                transform.rotation = mainCamera.transform.rotation;
                // ���W�w��̐ݒ�����[�J�����W�n�ɂ������߁A�^������W�ɂ����������
            }
        }

        if (Input.GetMouseButtonUp(0) && isTouch)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit2d = Physics2D.Raycast(ray.origin, ray.direction, maxDistance);
            //endPos.position = ray.origin;
            if (hit2d.collider.gameObject.layer == 7 || hit2d.collider.gameObject.layer == 8)
            {
                Debug.Log("aa");

                pos = Input.mousePosition;
                pos.z = 0.2f;

                // �}�E�X�X�N���[�����W�����[���h���W�ɒ���
                pos = mainCamera.ScreenToWorldPoint(pos);

                //// ����ɂ�������[�J�����W�ɒ����B
                pos = transform.InverseTransformPoint(pos);

               // lineRenderer.SetPosition(1, pos);
                isTouch = false;
                IsCollision = false;
                pos.z = 0;
                lineRenderer.SetPosition(1, pos);
                Debug.Log(lineRenderer.transform.position);
            }
            else if (hit2d.collider.gameObject.layer == 9)
            {
                lineManager.createLineCount++;
                Destroy(this.gameObject);
            }

        }
    }
}

