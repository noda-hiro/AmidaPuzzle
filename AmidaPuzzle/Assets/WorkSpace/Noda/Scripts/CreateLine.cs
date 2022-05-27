using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateLine : MonoBehaviour
{
    private Vector3 m_mouseDownPosition = Vector3.zero;

    void OnMouseDown()
    {
        // �}�E�X�N���b�N�����ۂ̏����ʒu��ۑ��B
        m_mouseDownPosition = transform.position;
    }

    void OnMouseDrag()
    {
        // �}�E�X�N���b�N�����ꏊ�����[���h���W�ɕω����āA
        // �����ʒu�ƃ}�E�X�N���b�N�ʒu�̒��ԂɃI�u�W�F�N�g��z�u�B
        // �I�u�W�F�N�g�̃X�P�[���������ʒu�ƃ}�E�X�N���b�N�̋����ɁB
        // �I�u�W�F�N�g�̌������}�E�X�N���b�N�����ʒu�ɁB

        Vector3 inputPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 9.5f);
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(inputPosition);
        Vector3 mediumPos = (mousePos - m_mouseDownPosition) / 2.0f;
        float dist = Vector3.Distance(mousePos, m_mouseDownPosition);

        transform.position = mediumPos;
        transform.localScale = new Vector3(1.0f, 1.0f, dist);
        transform.LookAt(mousePos);
        Debug.Log("aa");
    }

    void OnMouseUp()
    {
        // �ʒu�A��]�A�X�P�[�������ɖ߂��B
        transform.position = m_mouseDownPosition;
        transform.rotation = Quaternion.identity;
        transform.localScale = Vector3.one;
    }
}
