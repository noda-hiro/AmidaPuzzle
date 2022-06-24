using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneMnager : SingletonMonobehaviour<MySceneMnager>
{
    private List<GameManager> managerPrefab = new List<GameManager>();

    protected override void Awake()
    {
        base.Awake();
        InitAllManager();
    }

    /// <summary>
    /// 生成後アクティブをFalseに
    /// </summary>
    private void InitAllManager()
    {
        for (int i = 0; i < managerPrefab.Count; i++)
        {
            Instantiate(managerPrefab[i].gameObject, this.transform.parent);
            managerPrefab[i].gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 画面遷移
    /// </summary>
    /// <param name="sceneIndex"></param>
    public IEnumerator NextScene(int index)
    {
        SceneManager.LoadSceneAsync(index);
        yield return null;
        NextSceneLoaded(index);
        yield break;
    }

    /// <summary>
    /// ロードに成功していたら各シーンマネージャーをアクティブ化する
    /// </summary>
    /// <param name="index">SceneNumber</param>
    private void NextSceneLoaded(int index)
    {
        var selectScene = SceneManager.GetSceneByBuildIndex(index);
        var currentScene = SceneManager.GetActiveScene().name;
        if (selectScene.isLoaded != true)
        {
            Debug.Log("画面戦に失敗しました"
                    + "現在のSceneは" + currentScene + "です。"
                    + "入力された値のSceneは" + selectScene + "です。"
                    );
            return;
        }
        SetCurrentSceneManager(index, currentScene, selectScene.name);
    }

    /// <summary>
    /// シーンマネージャーをアクティブ化する
    /// </summary>
    /// <param name="managerPrefabNumber">ManagerPrefabでのナンバー</param>
    /// <param name="selectNumSceneName">現在のシーン名</param>
    /// <param name="currentSceneName">indexを代入して呼ばれたシーン名</param>
    public void SetCurrentSceneManager(int managerPrefabNumber, string currentSceneName, string selectNumSceneName)
    {
        for (int i = 0; i < managerPrefab.Count; i++)
        {
            managerPrefab[managerPrefabNumber].gameObject.SetActive(currentSceneName == selectNumSceneName);
        }
    }
}
