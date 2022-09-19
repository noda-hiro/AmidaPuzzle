using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageSelectController : MonoBehaviour
{
    [SerializeField] private List<Button> ButtonList = new List<Button>();
    [SerializeField] private Button titleButton;
    [SerializeField] private Button howToPlayButton;
    [SerializeField] private Button backlSelectButton;
    [SerializeField] private GameObject howToPlayPanel;
    private string Stage = "Stage";

    // Start is called before the first frame update
    void Start()
    {
        StageBtnSetting();
        OtherButtonSetting();
        howToPlayButton.onClick.AddListener(() => howToPlayPanel.SetActive(true));
        backlSelectButton.onClick.AddListener(() => howToPlayPanel.SetActive(false));
    }
    private void StageBtnSetting()
    {
        ButtonList[0].onClick.AddListener(() => SceneManager.LoadScene(Stage + 1));
        ButtonList[1].onClick.AddListener(() => SceneManager.LoadScene(Stage + 2));
        ButtonList[2].onClick.AddListener(() => SceneManager.LoadScene(Stage + 3));
        ButtonList[3].onClick.AddListener(() => SceneManager.LoadScene(Stage + 4));
        ButtonList[4].onClick.AddListener(() => SceneManager.LoadScene(Stage + 5));
        ButtonList[5].onClick.AddListener(() => SceneManager.LoadScene(Stage + 6));
    }

    private void OtherButtonSetting()
    {
        titleButton.onClick.AddListener(() => SceneManager.LoadScene("TitleScene"));
    }

}
