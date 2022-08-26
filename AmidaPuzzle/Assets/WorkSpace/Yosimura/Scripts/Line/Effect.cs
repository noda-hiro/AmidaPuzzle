using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    //当たった時のエフェクト
    public GameObject unit;

    //当たり判定メソッド
  void OnCollisionEnter2D(Collision2D collision)
    {

        //衝突したオブジェクトがblockだったとき
        if (collision.gameObject.CompareTag("Block"))
        {

            Debug.Log("aaaa");
            //エフェクトを発生させる
            GenerateEffect();
        }

    }

    //エフェクトを生成する
    void GenerateEffect()
    {
        //エフェクトを生成する
        GameObject effect = Instantiate(unit) as GameObject;
        //エフェクトが発生する場所を決定する(敵オブジェクトの場所)
        effect.transform.position = gameObject.transform.position;
    }
}
