using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClearScreenSetting : MonoBehaviour
{
    [SerializeField] private Button menuBtn = null;
    [SerializeField] private Button nextStageBtn = null;

    void Start()
    {
        menuBtn.onClick.AddListener(() => SceneManager.LoadScene("SelectStage"));
        nextStageBtn.onClick.AddListener(() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1));
    }
}
