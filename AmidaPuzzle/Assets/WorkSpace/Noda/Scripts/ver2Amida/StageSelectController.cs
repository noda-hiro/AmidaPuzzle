using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageSelectController : MonoBehaviour
{
    [SerializeField] private List<Button> ButtonList = new List<Button>();
    [SerializeField] private Button titleButton;
    // Start is called before the first frame update
    void Start()
    {
        StageBtnSetting();
        OtherButtonSetting();
    }
    private void StageBtnSetting()
    {
        ButtonList[0].onClick.AddListener(() => SceneManager.LoadScene("AmidaStage1"));
        ButtonList[1].onClick.AddListener(() => SceneManager.LoadScene("AmidaStage2"));
        ButtonList[2].onClick.AddListener(() => SceneManager.LoadScene("AmidaStage3"));
        //ButtonList[3].onClick.AddListener(() => SceneManager.LoadScene("AmidaStageFour"));
        //ButtonList[4].onClick.AddListener(() => SceneManager.LoadScene("AmidaStageFive"));
        ButtonList[5].onClick.AddListener(() => SceneManager.LoadScene("AmidaPuzzle"));
    }

    private void OtherButtonSetting()
    {
        titleButton.onClick.AddListener(() => SceneManager.LoadScene("TitleScene"));
    }
}
