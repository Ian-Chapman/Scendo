using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class FadeController : MonoBehaviour
{

    public Image blackFade;
    //public Image whiteFade;


    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(DelayAndFadeFromBlack());
        fadeFromBlack();

        //For fading from black on stage opening
        blackFade.canvasRenderer.SetAlpha(1.0f);

        // For Fading to black
       blackFade.canvasRenderer.SetAlpha(0.0f);


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void fadeFromBlack()
    {
        blackFade.CrossFadeAlpha(0, 1, false);
    }

    //public void fadeToWhite()
    //{
    //    whiteFade.CrossFadeAlpha(1, 1, false);
    //}

    //public void OnRunButtonPressed()
    //{
    //    StartCoroutine(delayAndFadeToWhite());
    //}


    IEnumerator DelayAndFadeFromBlack()
    {
        yield return new WaitForSeconds(.5f);
        fadeFromBlack();
    }

    //IEnumerator delayAndFadeToWhite()
    //{
    //    yield return new WaitForSeconds(1f);
    //    fadeToWhite();
    //}
}
