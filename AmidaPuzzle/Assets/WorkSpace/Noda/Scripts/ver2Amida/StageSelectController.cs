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
    [SerializeField] private noda.AudioManager audioManager;

    private string Stage = "Stage";

    // Start is called before the first frame update
    void Start()
    {

        howToPlayButton.onClick.AddListener(() =>
        {
            buttonActionSE();
            howToPlayPanel.SetActive(true);
        });
        backlSelectButton.onClick.AddListener(() =>
        {
            buttonActionSE();
            howToPlayPanel.SetActive(false);
        });
        StageBtnSetting();
        OtherButtonSetting();
    }
    private void StageBtnSetting()
    {

        ButtonList[0].onClick.AddListener(() => { StartCoroutine(StageLoad(1)); });
        ButtonList[1].onClick.AddListener(() => { StartCoroutine(StageLoad(2)); });
        ButtonList[2].onClick.AddListener(() => { StartCoroutine(StageLoad(3)); });
        ButtonList[3].onClick.AddListener(() => { StartCoroutine(StageLoad(4)); });
        ButtonList[4].onClick.AddListener(() => { StartCoroutine(StageLoad(5)); });
        ButtonList[5].onClick.AddListener(() => { StartCoroutine(StageLoad(6)); });
    }

    private IEnumerator StageLoad(int stageNum)
    {
        buttonActionSE();
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(Stage + stageNum);
        yield break;
    }

    private void OtherButtonSetting()
    {

        titleButton.onClick.AddListener(() => { buttonActionSE(); SceneManager.LoadScene("TitleScene");});
    }

    private void buttonActionSE()
    {
        audioManager.PlayStart("coalescenceButtonSE");
    }

}
