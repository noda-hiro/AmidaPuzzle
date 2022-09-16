using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangScene : MonoBehaviour
{
    public void OnClickStartButton()
    {
        if (SceneManager.GetActiveScene().name == "Ex_Window")
        { 
            SceneManager.LoadScene("UI_Window"); 
        }
        else if(SceneManager.GetActiveScene().name == "UI_Window")
        {
            SceneManager.LoadScene("Ex_Window");
        }
           
    }
}
