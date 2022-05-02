using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleScreenButtonManager : MonoBehaviour
{
    GameObject NewGameButton;
    GameObject ContinueButton;
    GameObject OptionsButton;
    GameObject HelpButton;
    GameObject CreditsButton;
    GameObject QuitButton;

    public Image blackFade;

    public AudioSource CassetteButton;
    public AudioSource CassetteStart;
    // Start is called before the first frame update
    void Start()
    {
        //For fading from black on stage opening
        blackFade.canvasRenderer.SetAlpha(1.0f);

        // For Fading to black
        blackFade.canvasRenderer.SetAlpha(0.0f);

        GameObject[] allButtons = UnityEngine.Object.FindObjectsOfType<GameObject>();

        foreach (GameObject go in allButtons)
        {
            if (go.name == "NewGameButton")
            {
                NewGameButton = go;
            }

            else if (go.name == "ContinueButton")
            {
                ContinueButton = go;
            }


            else if (go.name == "OptionsButton")
            {
                OptionsButton = go;
            }

            else if (go.name == "HelpButton")
            {
                HelpButton = go;
            }


            else if (go.name == "CreditsButton")
            {
                CreditsButton = go;
            }

            else if (go.name == "QuitButton")
            {
                QuitButton = go;
            }
        }

        NewGameButton.GetComponent<Button>().onClick.AddListener(NewGameButtonPressed);
        ContinueButton.GetComponent<Button>().onClick.AddListener(ContinueButtonPressed);
        HelpButton.GetComponent<Button>().onClick.AddListener(HelpButtonPressed);
        CreditsButton.GetComponent<Button>().onClick.AddListener(CreditsButtonPressed);
        QuitButton.GetComponent<Button>().onClick.AddListener(QuitButtonPressed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void NewGameButtonPressed()
    {
        Debug.Log("1");
        CassetteButton.Play();
        StartCoroutine(WaitToPlayOtherSFX());
        StartCoroutine(WaitToStartGame());
        StartCoroutine(DelayAndFadeToBlack());
    }

    void ContinueButtonPressed()
    {
        Debug.Log("2");
        CassetteButton.Play();
        StartCoroutine(WaitToPlayOtherSFX());
        StartCoroutine(WaitToLoadGame());
        StartCoroutine(DelayAndFadeToBlack());

    }

    void OptionsButtonPressed()
    {
        Debug.Log("3");
        CassetteButton.Play();

    }

    void HelpButtonPressed()
    {
        Debug.Log("4");
        StartCoroutine(ButtonDelayAndHelp());
    }

    void CreditsButtonPressed()
    {
        Debug.Log("5");
        StartCoroutine(ButtonDelayAndCredits());

    }

    void QuitButtonPressed()
    {
        Debug.Log("6");
        StartCoroutine(ButtonDelayAndQuit());

    }

    public void fadeToBlack()
    {
        //This is changing the FadeIn/Out image to 1 (0 = invisible / 1 = visible)
        // 2nd argument is the amount of time the fade takes to complete
        blackFade.CrossFadeAlpha(1, 3.0f, false);

    }

    public void fadeFromBlack()
    {
        blackFade.CrossFadeAlpha(0, 1, false);
    }

    IEnumerator WaitToPlayOtherSFX()
    {
        yield return new WaitForSeconds(.4f);
        CassetteStart.Play();
    }

    IEnumerator WaitToStartGame()
    {
        PlayerPrefs.SetInt("NewGameFlag", 1);
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(1);

    }

    IEnumerator WaitToLoadGame()
    {

        if (PlayerPrefs.HasKey("xPos") && PlayerPrefs.HasKey("yPos")) // do stuff if playerprefs has data to load
        {
            PlayerPrefs.SetInt("NewGameFlag", 0);
            PlayerPrefs.SetInt("ReturnFromEncounter", 0);
            yield return new WaitForSeconds(5f);
            SceneManager.LoadScene(1);
        }
        else
        {
            Debug.Log("no saved data found!");
        }


    }

    IEnumerator DelayAndFadeToBlack()
    {
        //wait 2 seconds for tape sound
        yield return new WaitForSeconds(2);
        fadeToBlack();
    }

    IEnumerator DelayAndFadeFromBlack()
    {
        yield return new WaitForSeconds(.5f);
        fadeFromBlack();
    }

    IEnumerator ButtonDelayAndCredits()
    {
        CassetteButton.Play();
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene("Credits");
    }

    IEnumerator ButtonDelayAndHelp()
    {
        CassetteButton.Play();
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene("Help");
    }

    IEnumerator ButtonDelayAndQuit()
    {
        CassetteButton.Play();
        yield return new WaitForSeconds(.5f);
        Application.Quit();
    }
}
