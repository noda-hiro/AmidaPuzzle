using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    [SerializeField] private Button startBtn;
    [SerializeField] private Button reStartBtn;
    [SerializeField] private Button reMenuBtn;
    // Start is called before the first frame update
    void Start()
    {
        if (startBtn != null)
            startBtn.onClick.AddListener(() => GotoGameScene());
        if (reStartBtn != null)
            reStartBtn.onClick.AddListener(() => ReSetScene());
        if (reMenuBtn != null)
            reMenuBtn.onClick.AddListener(() => ReMenuScene());
    }

    private void GotoGameScene()
    {
        SceneManager.LoadSceneAsync("SelectStage");
    }

    private void ReSetScene()
    {
        SceneManager.LoadSceneAsync("LineTest");
    }

    private void ReMenuScene()
    {
        SceneManager.LoadSceneAsync("TitleScene");
    }
}
