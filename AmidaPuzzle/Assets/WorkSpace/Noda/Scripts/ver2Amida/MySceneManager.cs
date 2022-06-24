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
    /// ������A�N�e�B�u��False��
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
    /// ��ʑJ��
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
    /// ���[�h�ɐ������Ă�����e�V�[���}�l�[�W���[���A�N�e�B�u������
    /// </summary>
    /// <param name="index">SceneNumber</param>
    private void NextSceneLoaded(int index)
    {
        var selectScene = SceneManager.GetSceneByBuildIndex(index);
        var currentScene = SceneManager.GetActiveScene().name;
        if (selectScene.isLoaded != true)
        {
            Debug.Log("��ʐ�Ɏ��s���܂���"
                    + "���݂�Scene��" + currentScene + "�ł��B"
                    + "���͂��ꂽ�l��Scene��" + selectScene + "�ł��B"
                    );
            return;
        }
        SetCurrentSceneManager(index, currentScene, selectScene.name);
    }

    /// <summary>
    /// �V�[���}�l�[�W���[���A�N�e�B�u������
    /// </summary>
    /// <param name="managerPrefabNumber">ManagerPrefab�ł̃i���o�[</param>
    /// <param name="selectNumSceneName">���݂̃V�[����</param>
    /// <param name="currentSceneName">index�������ČĂ΂ꂽ�V�[����</param>
    public void SetCurrentSceneManager(int managerPrefabNumber, string currentSceneName, string selectNumSceneName)
    {
        for (int i = 0; i < managerPrefab.Count; i++)
        {
            managerPrefab[managerPrefabNumber].gameObject.SetActive(currentSceneName == selectNumSceneName);
        }
    }
}
