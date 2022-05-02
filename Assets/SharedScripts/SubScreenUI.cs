using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SubScreenUI : MonoBehaviour
{
    private void Start()
    {
        
    }

    public void OnBackButtonPressed()
    {
        SceneManager.LoadScene("TitleScreen");
    }

    public void OnUIQuitButtonPressed()
    {
        SceneManager.LoadScene("TitleScreen");
    }
}
