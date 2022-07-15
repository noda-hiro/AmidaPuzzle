using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FailureScreenSetting : MonoBehaviour
{
    [SerializeField] private Button menuBtn = null;
    [SerializeField] private Button RetryBtn = null;
    [SerializeField] private GameObject prefab;
    // Start is called before the first frame update
    void Start()
    {
        menuBtn.onClick.AddListener(() => SceneManager.LoadScene("SelectStage"));
        RetryBtn.onClick.AddListener(() => Retry());
    }

    private void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Destroy(prefab);
    }
}
