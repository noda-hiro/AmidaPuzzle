using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    //�����������̃G�t�F�N�g
    public GameObject unit;

    //�����蔻�胁�\�b�h
  void OnCollisionEnter2D(Collision2D collision)
    {

        //�Փ˂����I�u�W�F�N�g��block�������Ƃ�
        if (collision.gameObject.CompareTag("Block"))
        {

            Debug.Log("aaaa");
            //�G�t�F�N�g�𔭐�������
            GenerateEffect();
        }

    }

    //�G�t�F�N�g�𐶐�����
    void GenerateEffect()
    {
        //�G�t�F�N�g�𐶐�����
        GameObject effect = Instantiate(unit) as GameObject;
        //�G�t�F�N�g����������ꏊ�����肷��(�G�I�u�W�F�N�g�̏ꏊ)
        effect.transform.position = gameObject.transform.position;
    }
}
