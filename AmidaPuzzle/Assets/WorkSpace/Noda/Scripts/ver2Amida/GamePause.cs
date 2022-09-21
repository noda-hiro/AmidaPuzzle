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
    private GameObject[] linePrefab;
    private WaitForSeconds waitTime = new WaitForSeconds(0.5f);
    // Start is called before the first frame update
    void Start()
    {
        pauseButton.onClick.AddListener(() => PauseGame());
        returnButton.onClick.AddListener(() => ReturnGame());
        titleButton.onClick.AddListener(() => ReturnSelectScreen());
        reStartButton.onClick.AddListener(() => StartCoroutine(ReStartGame()));
    }

    private void PauseGame()
    {
        audioManager.PlayStart("coalescenceButtonSE");
        linePrefab = GameObject.FindGameObjectsWithTag("AMIDA");
        for (int i = 0; i < linePrefab.Length; i++)
        {
            linePrefab[i].gameObject.SetActive(false);
        }
        panel.SetActive(true);
        Time.timeScale = 0;
    }

    private void ReturnGame()
    {
        audioManager.PlayStart("coalescenceButtonSE");
        for (int i = 0; i < linePrefab.Length; i++)
        {
            linePrefab[i].gameObject.SetActive(true);
        }
        panel.SetActive(false);
        Time.timeScale = 1;
    }

    private void ReturnSelectScreen()
    {
        audioManager.PlayStart("coalescenceButtonSE");
        ReturnGame();
        SceneManager.LoadScene("SelectStage");
    }

    private IEnumerator ReStartGame()
    {
        audioManager.PlayStart("coalescenceButtonSE");
        //s ReturnGame();
        yield return waitTime;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        yield break;
    }
}
