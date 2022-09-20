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
    [SerializeField] private Button reStartButton = null;
    [SerializeField] private GameObject panel;
    [SerializeField] private noda.AudioManager audioManager;
    // Start is called before the first frame update
    void Start()
    {
        pauseButton.onClick.AddListener(() => PauseGame());
        returnButton.onClick.AddListener(() => ReturnGame());
        titleButton.onClick.AddListener(() => ReturnSelectScreen());
        reStartButton.onClick.AddListener(() => ReStartGame());
    }

    private void PauseGame()
    {
        audioManager.PlayStart("coalescenceButtonSE");
        panel.SetActive(true);
        Time.timeScale = 0;
    }

    private void ReturnGame()
    {
        audioManager.PlayStart("coalescenceButtonSE");
        panel.SetActive(false);
        Time.timeScale = 1;
    }

    private void ReturnSelectScreen()
    {
        audioManager.PlayStart("coalescenceButtonSE");
        ReturnGame();
        SceneManager.LoadScene("SelectStage");
    }

    private void ReStartGame()
    {
        audioManager.PlayStart("coalescenceButtonSE");
        ReturnGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
