using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OverworldAIEncounterBehaviour : MonoBehaviour
{
    [SerializeField]
    List<AudioClip> tapeSoundClips;

    public GameObject player;

    public Image blackFade;

    public AudioSource tapeSounds;

    public GameObject spacebarIcon;


    // Start is called before the first frame update
    void Start()
    {

        //For fading from black on stage opening
        blackFade.canvasRenderer.SetAlpha(1.0f);

        // For Fading to black
        blackFade.canvasRenderer.SetAlpha(0.0f);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if ((other.tag == "Noah") && (Input.GetKeyDown("space")))
        {
            //disable player movement and animations
            player.GetComponent<PlayerMovement>().enabled = !player.GetComponent<PlayerMovement>().enabled;
            player.GetComponent<CharacterWalkAnimController>().enabled = !player.GetComponent<CharacterWalkAnimController>().enabled;

            //turn off the space bar icon
            spacebarIcon.SetActive(false);
            
            StartCoroutine(DelayBeforeEncounter());
        }

    }

    public void fadeToBlack()
    {
        //This is changing the FadeIn/Out image to 1 (0 = invisible / 1 = visible)
        // 2nd argument is the amount of time the fade takes to complete
        blackFade.CrossFadeAlpha(1, 3.0f, false);

    }

    public IEnumerator DelayAndFadeToBlack()
    {
        //wait 2 seconds for tape sound
        yield return new WaitForSeconds(2);
        fadeToBlack();
    }

    public IEnumerator ReActivateMovementAndAnimations()
    {
        yield return new WaitForSeconds(5);
        //Re-enable player movement
        player.GetComponent<PlayerMovement>().enabled = !player.GetComponent<PlayerMovement>().enabled;
        player.GetComponent<CharacterWalkAnimController>().enabled = !player.GetComponent<CharacterWalkAnimController>().enabled;
    }

    public IEnumerator WaitToPlayOtherSFX()
    {
        yield return new WaitForSeconds(.4f);
        tapeSounds.clip = tapeSoundClips[1];
        tapeSounds.Play();
    }

    public IEnumerator WaitToStartEncounter()
    {

        PlayerPrefs.SetInt("EncounterCheck", 1);
        PlayerPrefs.SetFloat("tempXPos", this.gameObject.transform.position.x);
        PlayerPrefs.SetFloat("tempYPos", this.gameObject.transform.position.y);
        PlayerPrefs.SetString("RandomEncounter", "Mourntooth");
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("EncounterScene");
        
        
    }


    public IEnumerator DelayBeforeEncounter()
    {
        //First the text prints
        //And we give the player 2.5 seconds to read it
        Debug.Log("DelayBeforeEncounter has started");
        yield return new WaitForSeconds(2.5f);
        
        
        //play the button press
        tapeSounds.clip = tapeSoundClips[0];
        tapeSounds.Play();
        StartCoroutine(WaitToPlayOtherSFX());
        StartCoroutine(WaitToStartEncounter());
        StartCoroutine(DelayAndFadeToBlack());
        StartCoroutine(ReActivateMovementAndAnimations());
        //load scene

    }

}
