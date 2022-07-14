using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePause : MonoBehaviour
{
    [SerializeField] private Button pauseButton = null;
    [SerializeField] private Button returnButton = null;
    [SerializeField] private Button titleButton = null;
    [SerializeField] private GameObject panel;
    // Start is called before the first frame update
    void Start()
    {
        pauseButton.onClick.AddListener(() => PauseGame());
        returnButton.onClick.AddListener(() => ReturnGame());
        titleButton.onClick.AddListener(() => SceneManager.LoadScene("SelectStage"));
    }

    private void PauseGame()
    {
        panel.SetActive(true);
        Time.timeScale = 0;
    }

    private void ReturnGame()
    {
        panel.SetActive(false);
        Time.timeScale = 1;
    }

}
