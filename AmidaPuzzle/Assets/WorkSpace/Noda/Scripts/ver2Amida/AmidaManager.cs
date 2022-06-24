using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum AmidaGameStateType
{
    PREPA,
    START,
    GOOL,
}

public class AmidaManager : SingletonMonobehaviour<AmidaManager>
{
    AmidaGameStateType amidaGameState = AmidaGameStateType.PREPA;
    MySceneMnager sceneMnager = MySceneMnager.Instance;
    [SerializeField] private Button startButton = null;


    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        StartCoroutine(AmidaGameState());
        startButton.onClick.AddListener(() => amidaGameState = AmidaGameStateType.START);
    }

    private IEnumerator AmidaGameState()
    {
        //スタートボタンが押されるまでの処理
        yield return new WaitUntil(() => amidaGameState == AmidaGameStateType.START);

        yield return new WaitUntil(() => amidaGameState == AmidaGameStateType.GOOL);
        sceneMnager.NextScene(1);
    }


}
